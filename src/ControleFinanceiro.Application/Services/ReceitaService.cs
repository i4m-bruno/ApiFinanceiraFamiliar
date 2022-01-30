using AutoMapper;
using ControleFinanceiro.Application.DTOs;
using ControleFinanceiro.Application.DTOs.Receita;
using ControleFinanceiro.Application.Interfaces;
using ControleFinanceiro.Domain.Entities;
using ControleFinanceiro.Domain.Repositories;

namespace ControleFinanceiro.Application.Services
{
    public class ReceitaService : IReceitaService
    {
        private readonly IReceitaRepository _receitaRepository;
        private readonly IMapper _mapper;

        public ReceitaService(IReceitaRepository receitaRepository, IMapper mapper)
        {
            _receitaRepository = receitaRepository;
            _mapper = mapper;
        }

        public async Task<ResponseDto<ReceitaDto>> CreateReceitaAsync(CreateReceitaDto receitaDto)
        {
            ResponseDto<ReceitaDto> response = new();            

            var receita = await _receitaRepository.CreateAsync(_mapper.Map<Receita>(receitaDto));
            
            response.Data = _mapper.Map<ReceitaDto>(receita); 

            return response;
        }


        public async Task<ResponseDto<IEnumerable<ReceitaDto>>> GetAllReceitasAsync(string? descricao)
        {
            ResponseDto<IEnumerable<ReceitaDto>> response = new();
            IEnumerable<Receita> receitas;

            if(string.IsNullOrEmpty(descricao))
                receitas = await _receitaRepository.GetAllAsync();
            else
                receitas = await  _receitaRepository.GetAllAsync(x => x.Descricao.Contains(descricao));
            
            response.Data = _mapper.Map<IEnumerable<ReceitaDto>>(receitas);
            return response;
        }

        public async Task<ResponseDto<IEnumerable<ReceitaDto>>> GetAllReceitasByDataAsync(int ano, int mes)
        {
            ResponseDto<IEnumerable<ReceitaDto>> response = new();

            var receitas = await _receitaRepository.GetAllAsync(x => x.Data.Year == ano && x.Data.Month == mes);

            response.Data = _mapper.Map<IEnumerable<ReceitaDto>>(receitas);
            return response;
        }

        public async Task<ReceitaDto> GetReceitaByIdAsync(int id)
        {
            var receita = await _receitaRepository.GetByIdAsync(id);
            return _mapper.Map<ReceitaDto>(receita);
        }

        public async Task<ReceitaDto> UpdateReceitaAsync(int id, CreateReceitaDto ReceitaDto)
        {
            var receita = await _receitaRepository.GetByIdAsync(id);
            
            if (receita is null) return null;
            
            await _receitaRepository.UpdateAsync(_mapper.Map(ReceitaDto, receita));

            return await GetReceitaByIdAsync(id);
        }

        public async Task<bool> DeleteReceitaAsync(int id)
        {
            var receita = await _receitaRepository.GetByIdAsync(id);

            if(receita is null) return false;

            await _receitaRepository.DeleteAsync(id);

            return true;
        } 
    }
}