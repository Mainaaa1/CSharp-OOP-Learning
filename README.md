# C# & ASP.NET Core Learning Journey

## Day 1 — C# Syntax & OOP

**Date:** Mon, 16 Feb
**Focus:** Core C# fundamentals and Object-Oriented Programming

---

## Overview

Today’s session focused on understanding the foundations of C# and mastering core Object-Oriented Programming (OOP) principles including:

* Classes & Objects
* Constructors
* Encapsulation
* Inheritance
* Polymorphism
* Abstraction
* Interfaces
* Clean separation of concerns

The goal was to structure code in a maintainable, scalable, and idiomatic C# way.

---

## Project Structure

```
CSharp-OOP-Learning/
│
├── Program.cs
├── Models/
│   ├── Person.cs
│   ├── Student.cs
│   ├── Instructor.cs
│
├── Interfaces/
│   └── IPrintable.cs
│
└── Services/
    └── ConsolePrinter.cs
```

---

## Concepts Demonstrated

### Encapsulation

Properties use private setters to protect object state.

```csharp
public string Name { get; private set; }
```

---

### Abstraction

`Person` is an abstract class that forces derived classes to implement required behavior.

```csharp
public abstract class Person
{
    public abstract string GetDetails();
}
```

---

### Inheritance

`Student` and `Instructor` inherit from `Person`.

```csharp
public class Student : Person
```

---

### Polymorphism

A base class reference can point to a derived object.

```csharp
Person personRef = student;
personRef.Introduce();
```

---

### Interfaces

`IPrintable` defines a contract implemented by `ConsolePrinter`.

```csharp
public interface IPrintable
{
    void Print(string message);
}
```

---

### Separation of Concerns

* Models → Domain entities
* Interfaces → Contracts
* Services → Implementations
* Program → Application entry point

This mirrors clean architecture principles used in ASP.NET Core applications.

---

## How to Run

### Prerequisites

Install .NET SDK and confirm installation:

```
dotnet --version
```

### Run the Project

```bash
dotnet new console -n CSharpOOP
cd CSharpOOP
# Replace generated files with this project structure

dotnet run
```

---

## Example Output

```
=== C# OOP Practice ===

Student: Ian, Age: 24, Course: Software Engineering
Instructor: Dr. Smith, Age: 45, Department: Computer Science

Polymorphism Demo:
Hi, I'm Ian, studying Software Engineering.
```

---

## Design Decisions

* Used an abstract base class to enforce shared structure
* Applied private setters to protect object integrity
* Separated printing logic from domain models
* Demonstrated polymorphism using a base reference type
* Avoided static/global state

This structure reflects patterns commonly used in ASP.NET Core applications.

---

## Upcoming Schedule

* Tue — ASP.NET Routing
* Wed — Controllers + Services
* Thu — Entity Framework Core
* Fri — API Mini-Project
* Sat — Build Events Booking API

The objective is to evolve from C# fundamentals into production-ready Web API development.

---

## Key Takeaways

* OOP in C# is strongly convention-driven
* Clean separation early prevents architectural debt later
* Abstract classes and interfaces are foundational for scalable backend systems
* Writing structured examples builds architectural intuition

---

## Reflection

Today focused on writing expressive, maintainable, and idiomatic C# code.
A strong grasp of OOP fundamentals will make ASP.NET Core significantly easier moving forward.

---

**Author:** Ian
**Track:** Backend Engineering (C# / ASP.NET Core)
