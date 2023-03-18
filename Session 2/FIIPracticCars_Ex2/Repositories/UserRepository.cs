using FIIPracticCars.Entities;
using FIIPracticCars.Repositories.Dtos;

namespace FIIPracticCars.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly CarsContext _context;

        public UserRepository(CarsContext context)
        {
            _context = context;
        }

        public void CreateUser(UserDto userDto)
        {
            if (userDto == null) throw new ArgumentNullException(nameof(userDto));
            if (string.IsNullOrEmpty(userDto.FirstName)) throw new ArgumentException($"{nameof(userDto.FirstName)} cannot be null or empty.");
            if (string.IsNullOrEmpty(userDto.LastName)) throw new ArgumentException($"{nameof(userDto.LastName)} cannot be null or empty.");
            if (string.IsNullOrEmpty(userDto.Email)) throw new ArgumentException($"{nameof(userDto.Email)} cannot be null or empty.");

            if (_context.Users.Any(u => u.Email == userDto.Email))
            {
                throw new Exception("Cannot insert a new User with the same Email.");
            }

            var userEntity = new User
            {
                FirstName = userDto.FirstName,
                LastName = userDto.LastName,
                Email = userDto.Email,
                BirthDate = userDto.BirthDate,
                PasswordHash = userDto.PasswordHash,
                RegistrationDate = DateTime.UtcNow,
            };

            _context.Users.Add(userEntity);
        }

        public void DeleteUser(int userId)
        {
            if (userId <= 0) throw new ArgumentOutOfRangeException(nameof(userId));

            var userToDelete = _context.Users.Find(userId);

            if (userToDelete != null)
            {
                _context.Users.Remove(userToDelete);
            }
        }

        public IEnumerable<UserDto> getAllUsers()
        {
            return _context.Users
                .Select(u => new UserDto
                {
                    Id = u.Id,
                    FirstName = u.FirstName,
                    LastName = u.LastName,
                    Email = u.Email,
                    BirthDate = u.BirthDate,
                    PasswordHash = u.PasswordHash
                })
                .ToList();


        }

        public UserDto? GetUser(int userId)
        {
            if (userId <= 0) return null;

            var user = _context.Users
                .FirstOrDefault(u => u.Id == userId);
            //.Select(u => new UserDto
            //{
            //    Id = u.Id,
            //    FirstName = u.FirstName,
            //    LastName = u.LastName,
            //    Email = u.Email,
            //    BirthDate = u.BirthDate,
            //    PasswordHash = u.PasswordHash
            //})

            if (user == null) return null;

            return new UserDto
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                BirthDate = user.BirthDate,
                PasswordHash = user.PasswordHash
            };
        }

        public IEnumerable<UserDto> SearchByName(string searchTerm)
        {
            return _context.Users
               .Where(u => u.FirstName.Contains(searchTerm) || u.LastName.Contains(searchTerm))
               .Select(u => new UserDto
               {
                   Id = u.Id,
                   FirstName = u.FirstName,
                   LastName = u.LastName,
                   Email = u.Email,
                   BirthDate = u.BirthDate,
                   PasswordHash = u.PasswordHash
               })
               .ToList();
        }

        public void Update(UserDto userDto)
        {
            if (userDto == null) return;
            if (string.IsNullOrEmpty(userDto.FirstName)) throw new ArgumentException($"{nameof(userDto.FirstName)} cannot be null or empty.");
            if (string.IsNullOrEmpty(userDto.LastName)) throw new ArgumentException($"{nameof(userDto.LastName)} cannot be null or empty.");
            if (string.IsNullOrEmpty(userDto.Email)) throw new ArgumentException($"{nameof(userDto.Email)} cannot be null or empty.");

            var user = GetUser(userDto.Id);

            if (user == null) throw new Exception("No usr found");


            user.FirstName = userDto.FirstName;
                user.LastName = userDto.LastName;
                user.Email = userDto.Email;
                user.BirthDate = userDto.BirthDate;
                user.PasswordHash = userDto.PasswordHash;
                //user.RegistrationDate = DateTime.UtcNow;
 
        }
    }
}
