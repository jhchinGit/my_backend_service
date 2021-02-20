namespace MyBackendService.Models
{
    public class RangeDto<T>
    {
        public T Start { get; set; }
        public T End { get; set; }
    }
}