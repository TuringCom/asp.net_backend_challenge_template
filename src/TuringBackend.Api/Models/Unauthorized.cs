namespace TuringBackend.Models
{
    public class Unauthorized
    {
        /// <summary>
        ///     example: UAT_02
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        ///     example: The apikey is invalid.
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        ///     example: API-KEY
        /// </summary>
        public string Field { get; set; }
    }
}