using Domain.Roles;

namespace Domain.Users;

public class User
{
    public UserId Id { get; }
    public string FullName { get; private set; }
    public string Email { get; private set; }
    public string PhoneNumber { get; private set; }
    public DateTime BirthDate { get; private set; } 
    public string Password { get; private set; }
    public Role? Role { get; private set; }
    public RoleId RoleId { get;  private set; }

    public User(UserId id, string fullName, string email, string password, RoleId roleId, 
        string phoneNumber, DateTime birthDate)
    {
        Id = id;
        FullName = fullName;
        Email = email;  
        Password = password;
        RoleId = roleId;
        PhoneNumber = phoneNumber;
        BirthDate = birthDate;
    }
    
    public static User  New(UserId id, string fullName, string email, string password, RoleId roleId, string phoneNumber, DateTime birthDate)
        => new(id, fullName, email, password, roleId, phoneNumber, birthDate);

    public void UpdateDetails(string fullName, string email, string phoneNumber, DateTime birthDate)
    {
        FullName = fullName;
        Email = email;
        PhoneNumber = phoneNumber;
        BirthDate = birthDate;
    }
    
    public void UpdatePassword(string password)
    {
        Password = password;
    } 
    public void UpdateEmail(string email)
    {
        Email = email;
    } 
    public void SetNewRole(RoleId roleId)
    {
        RoleId = roleId;
    }
}