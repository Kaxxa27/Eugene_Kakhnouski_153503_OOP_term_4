namespace TaxiApp.Application.Abstractions; 

internal interface IContactTechnicalSupport 
{
    // Реализовать обращение в тех. поддержку.
    // Пользователь отсылает сообщение о проблеме и ему присылают ответ.
    Task/*<Responde>*/ ContactTechnicalSupport(/*Messege messege*/);
}
