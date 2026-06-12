

namespace API.Talabat.Errors
{
    public class ApiResponse
    {
        public int StatusCode {  get; set; }
        public string? Message { get; set; }
        public ApiResponse(int statuscode,string? maessge=null)
        {
            StatusCode = statuscode;
            Message = maessge??GenerateDefaultMessageOfStatusCode(statuscode);
        }

        private string? GenerateDefaultMessageOfStatusCode(int? statuscode)
        {
            return StatusCode switch
            {
                400 => "A bad Request ,you have made ",
                401=>"Authorized, you are not",
                404=>"Resource was not Found",
                500=>"Errors are the bath to the dark side (Server Error)"
            };


    }
}
}
