using System.ComponentModel.DataAnnotations;

namespace Backend_SafeSpace
{
    public class Professional
    {
       
        public int Id { get; set; }  // Add an Id as primary key

        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }

        public string TypeOfProfessional { get; set; }  // Note: Typo in "Professional"
        public string Image { get; set; }
    }
}
