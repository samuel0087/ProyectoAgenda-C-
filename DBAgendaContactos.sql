CREATE DATABASE DBAgendaContactos;
USE DBAgendaContactos;
CREATE TABLE Contactos (
    Id_Contacto int not null IDENTITY(1, 1) PRIMARY KEY,
    Apellido varchar(50) NOT NULL,
    Nombre varchar(50) NOT NULL,
    Telefono varchar(15) NOT NULL
    
);

