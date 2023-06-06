using Entities;
using ServiceContracts;
using ServiceContracts.DTO;
using Services.Helpers;
using System.ComponentModel.DataAnnotations;

namespace Services
{
    public class PersonsService : IPersonsService
    {
        //private field
        private readonly List<Person> _persons;
        private readonly ICountriesService _countriesService;
        public PersonsService()
        {
            _persons = new List<Person>();
            _countriesService = new CountriesService();
        }

        private PersonResponse ConvertPersonToPersonResponse(Person person)
        {
            PersonResponse personResponse = person.ToPersonResponse();
            personResponse.Country = _countriesService.GetCountryByCountryId(person.CountryId)?.CountryName;
            return personResponse;
        }

        public PersonResponse AddPerson(PersonAddRequest? personAddRequest)
        {
            //check if PersonAddRequest is not null
            if(personAddRequest == null)
            {
                throw new ArgumentNullException(nameof(personAddRequest));
            }

            //Model validation
            ValidationHelper.ModelValidation(personAddRequest);

            //Convert personAddRequest into Person type
            Person person = personAddRequest.ToPerson();

            //Generate PersonId
            person.PersonId = Guid.NewGuid();

            //Add person object to the persons list
            _persons.Add(person);

            //Convert the Person object into PersonResponse type
            return ConvertPersonToPersonResponse(person);           
        }

        public List<PersonResponse> GetAllPersons()
        {
            throw new NotImplementedException();
        }

        public PersonResponse? GetPersonByPersonId(Guid? personId)
        {
            throw new NotImplementedException();
        }
    }
}
