using System;
using System.Collections.Generic;
using UMS_Lab5.Domain.Interfaces;

namespace UMS_Lab5.Persistence.Models;

public partial class Grade : ISubject
{
    private readonly List<IObserver> _observers = new();

    public int GradeId { get; set; }

    public long? StudentId { get; set; }

    public long? Teacherpercourseid { get; set; }

    public decimal? Grade1 { get; set; }

    public virtual User? Student { get; set; }

    public virtual Course? Teacherpercourse { get; set; }

    public void RegisterObserver(IObserver observer)
    {
        _observers.Add(observer);
    }

    public void UnregisterObserver(IObserver observer)
    {
        _observers.Remove(observer);
    }

    public void NotifyObservers()
    {
        foreach (var observer in _observers)
        {
            observer.Update(this);
        }
    }

    public void SetGrade(decimal grade)
    {
        Grade1 = grade;
        NotifyObservers();
    }
}

