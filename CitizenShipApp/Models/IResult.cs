namespace CitizenShipApp.Models
{
    public interface IResult<T>
    {
        T Data { get; set; }
        string ErrorMessage { get; set; }
        bool IsSucess { get; set; }
    }
}