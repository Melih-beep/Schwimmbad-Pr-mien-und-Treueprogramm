
CREATE TABLE Nutzer (
    NutzerID INT AUTO_INCREMENT PRIMARY KEY,     -- Primärschlüssel
    Name VARCHAR(100) NOT NULL,                  -- Name des Nutzers
    Email VARCHAR(100) UNIQUE NOT NULL,          -- E-Mail des Nutzers (unique)
    Punkte INT DEFAULT 0,                        -- Punkte des Nutzers
    Besuche INT DEFAULT 0                        -- Anzahl der Besuche
);


CREATE TABLE RabattFreikarte (
    RabattID INT AUTO_INCREMENT PRIMARY KEY,    -- Primärschlüssel
    Typ ENUM('Rabatt', 'Freikarte') NOT NULL,   -- Typ des Rabatts (Freikarte oder Rabatt)
    PunkteErforderlich INT NOT NULL             -- Punkte, die für den Rabatt oder Freikarte erforderlich sind
);


CREATE TABLE Nutzer_RabattFreikarte (
    NutzerID INT,                              -- Fremdschlüssel von der Nutzer-Tabelle
    RabattID INT,                              -- Fremdschlüssel von der RabattFreikarte-Tabelle
    PRIMARY KEY (NutzerID, RabattID),          -- Kombination aus NutzerID und RabattID als Primärschlüssel
    FOREIGN KEY (NutzerID) REFERENCES Nutzer(NutzerID) ON DELETE CASCADE, -- Fremdschlüssel
    FOREIGN KEY (RabattID) REFERENCES RabattFreikarte(RabattID) ON DELETE CASCADE -- Fremdschlüssel
);


INSERT INTO Nutzer (Name, Email, Punkte, Besuche) 
VALUES ('Melih', 'melih@gmail.com', 10, 10);

INSERT INTO RabattFreikarte (Typ, PunkteErforderlich) 
VALUES ('Rabatt', 5), 
       ('Freikarte', 10);


INSERT INTO Nutzer_RabattFreikarte (NutzerID, RabattID)
VALUES (1, 1); 
