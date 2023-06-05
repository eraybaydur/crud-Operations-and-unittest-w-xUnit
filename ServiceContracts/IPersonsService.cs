using Entities;
using ServiceContracts.DTO;
using System;



namespace ServiceContracts
{
    /// <summary>
    /// Represents business logic for manipulating Person entity
    /// </summary>
    public interface IPersonsService
    {

        /// <summary>
        /// Adds a new person int othe list of persons
        /// </summary>
        /// <param name="personAddRequest">Person to add</param>
        /// <returns>Returns the same person details, along with newly generated PersonId</returns>
        PersonResponse AddPerson(PersonAddRequest? personAddRequest);

        /// <summary>
        /// Returns all persons
        /// </summary>
        /// <returns>Returns a list of PersonResponse type</returns>
        List<PersonResponse> GetAllPersons();


    }
}
