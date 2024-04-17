namespace API.Dtos {
    public class UserToAddDto
     {
        // Getting rid of UserID to not be found in the Json Response when Post or Put
        // public int UserId { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string? Gender { get; set; }
        public bool Active { get; set; }
    }
}