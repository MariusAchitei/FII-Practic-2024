using FiiPracticCars.web.Models;
using FIIPracticCars;
using FIIPracticCars.Repositories;
using FIIPracticCars.Repositories.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace FiiPracticCars.web.Controllers;

public class UserController : Controller
{
    private readonly IUserRepository _userRepository;
    private readonly ICarsUnitOfWork _carsUnitOfWork;
    public UserController(IUserRepository userRepository, ICarsUnitOfWork carsUnitOfWork)
    {
        _userRepository = userRepository;
        _carsUnitOfWork = carsUnitOfWork;
    }
    public ActionResult Index()
    {
        var userDtos = _userRepository.getAllUsers() ?? new List<UserDto>();
        var userViewModels = userDtos.Select(u => new UserViewModel
        {
            Id = u.Id,
                Email = u.Email,
                FirstName = u.FirstName,
                LastName = u.LastName,
        });
        return View(userViewModels);
    }

    [HttpGet]
    public ActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public ActionResult Create([FromForm] CreateUserViewModel createUserViewModel)
    {
        var userDto = new UserDto {
        FirstName= createUserViewModel.FirstName,
        LastName= createUserViewModel.LastName,
        Email= createUserViewModel.Email,
        PasswordHash= createUserViewModel.PasswordHash,
        };

        _userRepository.CreateUser(userDto);
        _carsUnitOfWork.SaveChanges();

        return RedirectToAction("Index");
    }
    [HttpGet]
    public ActionResult Edit()
    {
        return View();
    }
}
