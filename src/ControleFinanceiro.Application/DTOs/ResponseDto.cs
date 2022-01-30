namespace ControleFinanceiro.Application.DTOs
{
    public class ResponseDto<TDto> where TDto : class
    {
        public bool Success { get; set; } = true;
        public TDto? Data { get; set; }
        public List<string> Erros { get; set; } = new();
    }
}