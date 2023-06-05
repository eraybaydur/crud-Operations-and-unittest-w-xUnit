using ServiceContracts;
using ServiceContracts.DTO;
using Services;
using Xunit;

namespace CRUDTests
{
    public class PersonsServiceTest
    {
        //private fields
        private readonly IPersonsService _personService;

        //constructor
        public PersonsServiceTest()
        {
            _personService = new PersonsService();
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
            Assert.Throws<ArgumentNullException>(() => _personService.AddPerson(personAddRequest));
        }

        //When we supply proper person details, it should insert the person into the persons list, and it should return an object of PersonResponse
        //which includes with the newly generated person id
        [Fact]
        public void AddPerson_ProperPersonDetails()
        {
            //Arrange
            PersonAddRequest? personAddRequest = new PersonAddRequest()
            {
                PersonName = null,
            };

            //Act
            Assert.Throws<ArgumentNullException>(() => _personService.AddPerson(personAddRequest));
        }
        #endregion

    }
}
