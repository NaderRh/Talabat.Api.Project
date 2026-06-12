namespace API.Talabat.Errors
{
    public class ApiValidationErrorResponse:ApiResponse
    {
        public IEnumerable<string> Errors { get; set; }
        public ApiValidationErrorResponse():base(statuscode:StatusCodes.Status400BadRequest)
        {
            Errors = new List<string>();
        }
    }
}
