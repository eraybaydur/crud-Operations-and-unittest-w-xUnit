using ServiceContracts;
using ServiceContracts.DTO;
using Services;
using Xunit;
using ServiceContracts.Enums;

namespace CRUDTests
{
    public class PersonsServiceTest
    {
        //private fields
        private readonly IPersonsService _personService;
        private readonly ICountriesService _countriesService;

        //constructor
        public PersonsServiceTest()
        {
            _personService = new PersonsService();
            _countriesService = new CountriesService();
        }

        #region AddPerson

        //When we supply null value as PersoneAddRequest, it should throw ArgumentNullException
        [Fact]
        public void AddPerson_NullPerson()
        {
            //Arrange
            PersonAddRequest? personAddRequest = null;

            //Act
            Assert.Throws<ArgumentNullException>(() => _personService.AddPerson(personAddRequest));
        }

        //When we supply null value as PersonName, it should throw ArgumentException
        [Fact]
        public void AddPerson_PersonNameIsNull()
        {
            //Arrange
            PersonAddRequest? personAddRequest = new PersonAddRequest()
            {
                PersonName = null,
            };

            //Act
            Assert.Throws<ArgumentException>(() => _personService.AddPerson(personAddRequest));
        }

        //When we supply proper person details, it should insert the person into the persons list, and it should return an object of PersonResponse
        //which includes with the newly generated person id
        [Fact]
        public void AddPerson_ProperPersonDetails()
        {
            //Arrange
            PersonAddRequest? personAddRequest = new PersonAddRequest()
            {
                PersonName = "Person name...",
                Email = "person@example.com",
                Address = "sample adress",
                CountryId = Guid.NewGuid(),
                Gender = GenderOptions.Male,
                DateOfBirth = DateTime.Parse("2000-01-01"),
                ReceiveNewsLetters = true,
            };

            //Act
            PersonResponse person_response_from_add = _personService.AddPerson(personAddRequest);

            List<PersonResponse> persons_list = _personService.GetAllPersons();

            //Assert
            Assert.True(person_response_from_add.PersonId != Guid.Empty);

            Assert.Contains(person_response_from_add, persons_list);
        }
        #endregion


        #region GetPersonById

        //If we supply null as PersonId, it should return null as PersonResponse
        [Fact]
        public void GetPersonByPersonId_NullPersonId()
        {
            //Arrange
            Guid? personId = null;

            //Act
            PersonResponse? person_response_from_get = _personService.GetPersonByPersonId(personId);

            //Assert
            Assert.Null(person_response_from_get);
        }

        //If we supply a valid person id, it should return the valid person details as PersonResponse object
        [Fact]
        public void GetPersonByPersonId_WithPersonId()
        {
            //Arrange
            CountryAddRequest country_request = new CountryAddRequest()
            {
                CountryName = "Canada"
            };

            CountryResponse country_response = _countriesService.AddCountry(country_request);

            //Act

            PersonAddRequest person_request = new PersonAddRequest()
            {
                PersonName = "person name...",
                Email = "email@sample.com",
                CountryId = country_response.CountryId,
                Address = "adress",
                DateOfBirth = DateTime.Parse("2000-10-10"),
                Gender = GenderOptions.Male,
                ReceiveNewsLetters = false
            };

            PersonResponse person_response_from_add = _personService.AddPerson(person_request);

            PersonResponse? person_response_from_get = _personService.GetPersonByPersonId(person_response_from_add.PersonId);

            //Assert
            Assert.Equal(person_response_from_add, person_response_from_get);

        }

        #endregion

        #region GetAllPersons
        //The GetAllPersons() return an empty list by default
        [Fact]
        public void GetAllPersons_EmptyList()
        {
            //Act
            List<PersonResponse> persons_from_get = _personService.GetAllPersons();

            //Assert
            Assert.Empty(persons_from_get);

        }

        //First, we will add few persons; and then when we call GetAllPersons(), it should return the same persons that were added.
        [Fact]
        public void GetAllPersons_AddFewPersons()
        {
            //Arrange
            CountryAddRequest country_request_1 = new CountryAddRequest() { CountryName = "USA" };
            CountryAddRequest country_request_2 = new CountryAddRequest() { CountryName = "India" };

            CountryResponse country_response_1 = _countriesService.AddCountry(country_request_1);
            CountryResponse country_response_2 = _countriesService.AddCountry(country_request_2);

            PersonAddRequest person_request_1 = new PersonAddRequest()
            {
                PersonName = "Smith",
                Email = "smith@example.com",
                Gender = GenderOptions.Male,
                Address = "address of smith",
                CountryId = country_response_1.CountryId,
                DateOfBirth = DateTime.Parse("2002-05-06"),
                ReceiveNewsLetters = true,
            };

            PersonAddRequest person_request_2 = new PersonAddRequest()
            {
                PersonName = "Mary",
                Email = "mary@example.com",
                Gender = GenderOptions.Female,
                Address = "address of mary",
                CountryId = country_response_2.CountryId,
                DateOfBirth = DateTime.Parse("2002-09-06"),
                ReceiveNewsLetters = false,
            };

            PersonAddRequest person_request_3 = new PersonAddRequest()
            {
                PersonName = "Rahman",
                Email = "rahman@example.com",
                Gender = GenderOptions.Male,
                Address = "address of rahman",
                CountryId = country_response_2.CountryId,
                DateOfBirth = DateTime.Parse("1991-05-11"),
                ReceiveNewsLetters = true,
            };

            List<PersonAddRequest> person_requests = new List<PersonAddRequest>()
            {
                person_request_1,
                person_request_2,
                person_request_3,
            };

            List<PersonResponse> person_response_list_from_add = new List<PersonResponse>();

            foreach(PersonAddRequest person_request in person_requests)
            {
                PersonResponse person_response = _personService.AddPerson(person_request);
                person_response_list_from_add.Add(person_response);
            }

            //Act
            List<PersonResponse> persons_list_from_get = _personService.GetAllPersons();

            //Assert
            foreach(PersonResponse person_response_from_add in person_response_list_from_add)
            {
                Assert.Contains(person_response_from_add, persons_list_from_get);
            }
            
        }
        #endregion

    }
}
