using SharedKernel.Dispatchers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CQRS.CarRental.Core.Commands
{
    /// <summary>
    /// Class with parameters to create new Driver
    /// </summary>
    public class CreateDriverCommand : ICommand
    {
        [Required]
        public string LicenceNumber { get; set; }
        [Required]
        [StringLength(50,ErrorMessage ="Field FirstName is too long")]
        public string FirstName { get; set; }
        [StringLength(50,ErrorMessage ="Field LastName is too long")]
        [Required]
        public string LastName { get; set; }

        public string GetFullName()
        {
            return $"{FirstName} {LastName}";
        }
    }
}
