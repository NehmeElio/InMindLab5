using UMS_Lab5.Persistence.Models;

namespace UMS_Lab5.Domain.Interfaces;

public interface IObserver
{
    void Update(Grade grade);
}