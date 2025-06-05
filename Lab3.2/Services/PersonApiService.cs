using Google.Protobuf.WellKnownTypes;
using Grpc.Core;

namespace Lab3._2.Services
{
    /*
     *   rpc ServerDataStream (Request) returns (stream Response);
         rpc ClientDataStream (stream Request) returns (Response);
         rpc DataStream (stream Request) returns (stream Response);

     * */
    public class PersonApiService : PeopleService.PeopleServiceBase
    {
        private readonly ApplicationContext _context;

        public PersonApiService(ApplicationContext context) // DI
        {
            _context = context;
        }


        //static List<Person> People = new() 
        //{ 
        //    new Person(1, "Sergey", 33),
        //    new Person(2, "Fedor", 42) };
        //}
        public override Task<ListReply> ListPeople(Empty request,
            //IServerStreamWriter<Response> writer,
            ServerCallContext context)
        {
            //using ApplicationContext applicationContext = new ApplicationContext();
            var listReply = new ListReply();
            var PersonList = _context.People.Select(
                item => new PersonReply { Id = item.Id, Name = item.Name, Age = item.Age }).ToList();
            listReply.People.AddRange(PersonList);
            return Task.FromResult(listReply);
        }
        public override Task<PersonReply> GetPerson(GetPersonRequest request, ServerCallContext context)
        {
            //using ApplicationContext applicationContext = new ApplicationContext();
            var Person = _context.People.Find(request.Id);
            
            if (Person == null)
                throw new RpcException(new Status(StatusCode.NotFound, "Person not found"));
            PersonReply PersonReply = 
                new PersonReply() { Id = Person.Id, Name = Person.Name, Age = Person.Age };
            return Task.FromResult(PersonReply);
        }
        public override Task<PersonReply> CreatePerson(CreatePersonRequest request, ServerCallContext context)
        {
            //using ApplicationContext applicationContext = new ApplicationContext();
            var Person = new Person() { Name = request.Name, Age = request.Age };
            _context.People.Add(Person);
            _context.SaveChanges();
            var reply = new PersonReply() { Id = Person.Id, Name = Person.Name, Age = Person.Age };
            return Task.FromResult(reply);
        }
        public override Task<PersonReply> UpdatePerson(UpdatePersonRequest request, ServerCallContext context)
        {
            //using ApplicationContext applicationContext = new ApplicationContext();
            var Person = _context.People.Find(request.Id);

            if (Person == null)
                throw new RpcException(new Status(StatusCode.NotFound, "Person not found"));

            Person.Name = request.Name;
            Person.Age = request.Age;
            _context.SaveChanges();
            var reply = new PersonReply() { Id = Person.Id, Name = Person.Name, Age = Person.Age };
            return Task.FromResult(reply);
        }
        public override Task<PersonReply> DeletePerson(DeletePersonRequest request, ServerCallContext context)
        {
            //using ApplicationContext applicationContext = new ApplicationContext();
            var Person = _context.People.Find(request.Id);

            if (Person == null)
                throw new RpcException(new Status(StatusCode.NotFound, "Person not found"));
            _context.People.Remove(Person);
            _context.SaveChanges();
            var reply = new PersonReply() { Id = Person.Id, Name = Person.Name, Age = Person.Age };
            return Task.FromResult(reply);
        }
    }
}

