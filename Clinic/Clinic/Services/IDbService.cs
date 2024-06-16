using Clinic.Models;

namespace Clinic.Services;

public interface IDbService
{
    Task<ICollection<Patient>> GetPatientsData(string? clientLastName);
    Task<bool> DoesClientExist(int clientID);
    Task<bool> DoesEmployeeExist(int employeeID);
    Task AddNewOrder(Order order);
    Task<Medicament?> GetMedicamentById(int id);
    Task AddOrderPastries(IEnumerable<OrderPastry> orderPastries);
}