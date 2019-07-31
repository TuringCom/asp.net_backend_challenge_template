namespace TuringBackend.Models
{
    public class Error
    {
        public Error(int status, string code, string message, string field)
        {
            Status = status;
            Code = code;
            Message = message;
            Field = field;
        }

        public Error(string code, string message, string field)
        {
            Code = code;
            Message = message;
            Field = field;
        }

        public int Status { get; set; }

        /// <summary>
        ///     example: USR_02
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        ///     example: The field example is empty.
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        ///     example: example
        /// </summary>
        public string Field { get; set; }
    }
}