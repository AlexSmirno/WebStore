
namespace WebStoreServer.Models
{
    // Класс для описания результатов выполнения функции
    public class Result<T>
    {
        public bool IsSucceeded = true;
        public T? Data;
        public string ErrorMessage = "";
        public int ErrorCode;

        public Result()
        {

        }

        public Result(T data)
        {
            Data = data;
        }

        public Result(Exception ex)
        {
            IsSucceeded = false;
            ErrorMessage = ex.Message;
        }

    }
}
