CREATE TABLE Students (
    Id INTEGER PRIMARY KEY AUTOINCREMENT,
    FullName TEXT NOT NULL,
    [Group] TEXT NOT NULL,
    GPA REAL NOT NULL
);

INSERT INTO Students (FullName, [Group], GPA) VALUES 
('Alice Smith', 'IT-21', 4.8),
('Bob Johnson', 'IT-22', 3.9),
('Charlie Brown', 'CS-11', 4.2);
