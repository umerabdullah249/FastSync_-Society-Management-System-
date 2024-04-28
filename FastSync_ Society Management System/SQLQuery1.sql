USE FastSync;
use master;
-- create Societies table
CREATE TABLE Societies (
    society_id INT PRIMARY KEY,
    name NVARCHAR(100) UNIQUE, -- Adding UNIQUE constraint to ensure uniqueness of society names
    mentor NVARCHAR(100),
    president_name NVARCHAR(100),
    description NVARCHAR(MAX)
);

-- Create Events Table
CREATE TABLE Events (
    event_id INT PRIMARY KEY,
    name NVARCHAR(100),
    date DATE,
    time TIME,
    society_id INT,
    FOREIGN KEY (society_id) REFERENCES Societies(society_id)
);
ALTER TABLE Events
ADD description NVARCHAR(MAX);

-- Create Users Table
CREATE TABLE Users (
    user_id INT PRIMARY KEY,
    username NVARCHAR(50),
    password NVARCHAR(100), -- Make sure to hash the passwords for security
    user_type NVARCHAR(20)
);

-- Create User-Society Relationship Table
CREATE TABLE User_Society_Relationship (
    user_id INT,
    society_id INT,
    FOREIGN KEY (user_id) REFERENCES Users(user_id),
    FOREIGN KEY (society_id) REFERENCES Societies(society_id),
    PRIMARY KEY (user_id, society_id)
);

CREATE TABLE SocietyRegistrationRequest (
    RequestID INT PRIMARY KEY IDENTITY,
    SocietyName NVARCHAR(100),
    SocietyDescription NVARCHAR(255),
    RequestedBy NVARCHAR(100),
    RequestDate DATETIME DEFAULT GETDATE(),
    ApprovalStatus NVARCHAR(20) DEFAULT 'Pending'
);

CREATE TABLE EventRegistrationRequest (
    RequestID INT PRIMARY KEY IDENTITY,
    EventName NVARCHAR(100),
    EventDate DATETIME,
	EventTime Time,
    Organizer NVARCHAR(100),
    RequestDate DATETIME DEFAULT GETDATE(),
    ApprovalStatus NVARCHAR(20) DEFAULT 'Pending'
);
ALTER TABLE EventRegistrationRequest
ADD CONSTRAINT FK_Event_Organizer FOREIGN KEY (Organizer) REFERENCES Societies(name);


-- Insert data into Societies table
INSERT INTO Societies (society_id, name, mentor, president_name, description)
VALUES 
(1, 'Computer Science Society', 'Dr. John Smith', 'Alice Johnson', 'The Computer Science Society aims to foster a community of passionate learners and professionals in the field of computer science.'),
(2, 'Mathematics Club', 'Prof. Emily Davis', 'Bob Thompson', 'The Mathematics Club organizes events and activities to promote interest and understanding in mathematics.'),
(3, 'Photography Society', 'Sarah Lee', 'Michael Brown', 'The Photography Society provides resources and opportunities for photographers of all skill levels to learn and create.'),
(4, 'Literature Circle', 'Dr. David Miller', 'Emily Wilson', 'The Literature Circle celebrates literature through discussions, readings, and creative writing workshops.'),
(5, 'Environmental Club', 'Dr. Lisa Green', 'Jack Roberts', 'The Environmental Club is dedicated to raising awareness about environmental issues and promoting sustainable practices.'),
(6, 'Music Ensemble', 'Prof. Thomas Clark', 'Sophia Parker', 'The Music Ensemble brings together musicians to collaborate and perform various genres of music.'),
(7, 'Debating Society', 'James Harris', 'Emma Turner', 'The Debating Society hosts debates, discussions, and public speaking events on a wide range of topics.'),
(8, 'Art Club', 'Ms. Julia White', 'Oliver Anderson', 'The Art Club provides a platform for artists to showcase their work and engage in artistic activities.'),
(9, 'Dance Team', 'Ms. Jessica Adams', 'Liam Smith', 'The Dance Team choreographs and performs dance routines for various events and competitions.'),
(10, 'Engineering Society', 'Dr. Michael Johnson', 'Sophie Brown', 'The Engineering Society promotes innovation and collaboration among engineering students.');


-- Insert data into Events table
INSERT INTO Events (event_id, name, date, time, society_id)
VALUES
(1, 'Hackathon 2024', '2024-04-15', '10:00:00', 1),
(2, 'Mathematics Quiz Competition', '2024-04-20', '14:30:00', 2),
(3, 'Photography Workshop', '2024-04-25', '13:00:00', 3),
(4, 'Literature Symposium', '2024-05-01', '11:00:00', 4),
(5, 'Environmental Awareness Campaign', '2024-05-10', '09:00:00', 5),
(6, 'Music Concert', '2024-05-15', '19:00:00', 6),
(7, 'Debate Tournament', '2024-05-20', '15:00:00', 7),
(8, 'Art Exhibition', '2024-06-01', '10:00:00', 8),
(9, 'Dance Showcase', '2024-06-10', '18:00:00', 9),
(10, 'Engineering Symposium', '2024-06-15', '12:00:00', 10);


-- Insert data into Users table
INSERT INTO Users (user_id, username, password, user_type)
VALUES
(1, 'john_doe', 'hashed_password_1', 'admin'),
(2, 'alice_smith', 'hashed_password_2', 'member'),
(3, 'bob_thompson', 'hashed_password_3', 'member'),
(4, 'sarah_lee', 'hashed_password_4', 'member'),
(5, 'david_miller', 'hashed_password_5', 'admin'),
(6, 'lisa_green', 'hashed_password_6', 'member'),
(7, 'thomas_clark', 'hashed_password_7', 'member'),
(8, 'james_harris', 'hashed_password_8', 'admin'),
(9, 'julia_white', 'hashed_password_9', 'member'),
(10, 'jessica_adams', 'hashed_password_10', 'admin');


-- Insert data into User_Society_Relationship table
INSERT INTO User_Society_Relationship (user_id, society_id)
VALUES
(1, 1), (2, 1), (3, 2), (4, 3), (5, 4),
(6, 5), (7, 6), (8, 7), (9, 8), (10, 9),
(1, 10), (3, 10), (5, 10), (7, 10), (9, 10);

-- Insert more data into Users table
INSERT INTO Users (user_id, username, password, user_type)
VALUES
(11, 'emma_turner', 'hashed_password_11', 'member'),
(12, 'oliver_anderson', 'hashed_password_12', 'member'),
(13, 'liam_smith', 'hashed_password_13', 'member'),
(14, 'sophie_brown', 'hashed_password_14', 'member'),
(15, 'jack_roberts', 'hashed_password_15', 'member'),
(16, 'emily_wilson', 'hashed_password_16', 'member'),
(17, 'michael_brown', 'hashed_password_17', 'member'),
(18, 'sophia_parker', 'hashed_password_18', 'member'),
(19, 'olivia_jones', 'hashed_password_19', 'member'),
(20, 'noah_taylor', 'hashed_password_20', 'member');
UPDATE Users
SET user_type = 'president'
WHERE user_id BETWEEN 11 AND 20;


---------------------------------------------------------------------------------
-- Insert data into EventRegistrationRequest table
-- These insertions are consistent with the societies already present in the Societies table

-- For Hackathon 2024 organized by Computer Science Society
INSERT INTO EventRegistrationRequest (EventName, EventDate, EventTime, Organizer)
VALUES ('Hackathon 2024', '2024-04-15', '10:00:00', 'Computer Science Society');

-- For Mathematics Quiz Competition organized by Mathematics Club
INSERT INTO EventRegistrationRequest (EventName, EventDate, EventTime, Organizer)
VALUES ('Mathematics Quiz Competition', '2024-04-20', '14:30:00', 'Mathematics Club');

-- For Photography Workshop organized by Photography Society
INSERT INTO EventRegistrationRequest (EventName, EventDate, EventTime, Organizer)
VALUES ('Photography Workshop', '2024-04-25', '13:00:00', 'Photography Society');

-- For Literature Symposium organized by Literature Circle
INSERT INTO EventRegistrationRequest (EventName, EventDate, EventTime, Organizer)
VALUES ('Literature Symposium', '2024-05-01', '11:00:00', 'Literature Circle');

-- For Environmental Awareness Campaign organized by Environmental Club
INSERT INTO EventRegistrationRequest (EventName, EventDate, EventTime, Organizer)
VALUES ('Environmental Awareness Campaign', '2024-05-10', '09:00:00', 'Environmental Club');

-- For Music Concert organized by Music Ensemble
INSERT INTO EventRegistrationRequest (EventName, EventDate, EventTime, Organizer)
VALUES ('Music Concert', '2024-05-15', '19:00:00', 'Music Ensemble');

-- For Debate Tournament organized by Debating Society
INSERT INTO EventRegistrationRequest (EventName, EventDate, EventTime, Organizer)
VALUES ('Debate Tournament', '2024-05-20', '15:00:00', 'Debating Society');

-- For Art Exhibition organized by Art Club
INSERT INTO EventRegistrationRequest (EventName, EventDate, EventTime, Organizer)
VALUES ('Art Exhibition', '2024-06-01', '10:00:00', 'Art Club');

-- For Dance Showcase organized by Dance Team
INSERT INTO EventRegistrationRequest (EventName, EventDate, EventTime, Organizer)
VALUES ('Dance Showcase', '2024-06-10', '18:00:00', 'Dance Team');

-- For Engineering Symposium organized by Engineering Society
INSERT INTO EventRegistrationRequest (EventName, EventDate, EventTime, Organizer)
VALUES ('Engineering Symposium', '2024-06-15', '12:00:00', 'Engineering Society');

----------------------------------------------------------------------------------------------
UPDATE Events
SET description = '';

UPDATE Events
SET description = 'A competitive event where participants develop software applications within a limited timeframe.'
WHERE event_id = 1;

UPDATE Events
SET description = 'A quiz competition focusing on various mathematical concepts and problem-solving skills.'
WHERE event_id = 2;

UPDATE Events
SET description = 'A hands-on workshop covering photography techniques, composition, and editing.'
WHERE event_id = 3;

UPDATE Events
SET description = 'An academic symposium discussing literature, literary theory, and critical analysis.'
WHERE event_id = 4;

UPDATE Events
SET description = 'An initiative to raise awareness about environmental issues and promote sustainable practices.'
WHERE event_id = 5;

UPDATE Events
SET description = 'A musical extravaganza featuring performances by talented musicians and bands.'
WHERE event_id = 6;

UPDATE Events
SET description = 'A competitive event where participants engage in debates on various topics, showcasing their argumentative skills.'
WHERE event_id = 7;

UPDATE Events
SET description = 'An exhibition showcasing artworks created by local artists, ranging from paintings to sculptures.'
WHERE event_id = 8;

UPDATE Events
SET description = 'A captivating showcase of dance performances, featuring various styles from classical to contemporary.'
WHERE event_id = 9;

UPDATE Events
SET description = 'A symposium highlighting innovations and advancements in the field of engineering, with presentations and discussions by experts.'
WHERE event_id = 10;


INSERT INTO Users (user_id, username, password, user_type)
VALUES
(21, 'prof_emily_davis', 'hashed_password_21', 'faculty'),
(22, 'dr_lisa_green', 'hashed_password_22', 'faculty'),
(23, 'prof_thomas_clark', 'hashed_password_23', 'faculty'),
(24, 'prof_james_harris', 'hashed_password_24', 'faculty'),
(25, 'dr_michael_johnson', 'hashed_password_25', 'faculty');

select * from Users

select * from EventRegistrationRequest

delete from events where society_id is null

ALTER TABLE EventRegistrationRequest
ADD description NVARCHAR(MAX);

update EventRegistrationRequest
set description = 'dummy'


INSERT INTO EventRegistrationRequest (EventName, EventDate, EventTime, Organizer, description)
VALUES ('adfdf', '2024-05-15', '19:00:00', 'Music Ensemble','dumb1');

INSERT INTO SocietyRegistrationRequest (SocietyName, SocietyDescription, RequestedBy)
VALUES ('Computer Science Club', 'A club for computer enthusiasts to learn and collaborate.', 'John Doe');

INSERT INTO SocietyRegistrationRequest (SocietyName, SocietyDescription, RequestedBy)
VALUES ('Art Society', 'A society for artists to showcase their work and participate in workshops.', 'Alice Smith');

INSERT INTO SocietyRegistrationRequest (SocietyName, SocietyDescription, RequestedBy)
VALUES ('Music Club', 'A club for music lovers to share their passion and perform together.', 'Bob Thompson');

INSERT INTO SocietyRegistrationRequest (SocietyName, SocietyDescription, RequestedBy)
VALUES ('Environmental Awareness Group', 'A group dedicated to promoting environmental awareness and sustainability.', 'Sarah Lee');

INSERT INTO SocietyRegistrationRequest (SocietyName, SocietyDescription, RequestedBy)
VALUES ('Literature Club', 'A club for book lovers to discuss and explore literature.', 'Emily Wilson');

INSERT INTO SocietyRegistrationRequest (SocietyName, SocietyDescription, RequestedBy)
VALUES ('Photography Society', 'A society for photography enthusiasts to share their passion and improve their skills.', 'Michael Brown');

INSERT INTO SocietyRegistrationRequest (SocietyName, SocietyDescription, RequestedBy)
VALUES ('Debating Society', 'A society for students to engage in debates and improve their public speaking skills.', 'Emma Turner');

INSERT INTO SocietyRegistrationRequest (SocietyName, SocietyDescription, RequestedBy)
VALUES ('Film Appreciation Club', 'A club for cinema lovers to watch and discuss films from around the world.', 'Oliver Anderson');



INSERT INTO Users (user_id, username, password, user_type)
VALUES
(21, 'emma_turner_21', 'hashed_password_21', 'president'),
(22, 'oliver_anderson_22', 'hashed_password_22', 'president'),
(23, 'liam_smith_23', 'hashed_password_23', 'president'),
(24, 'sophie_brown_24', 'hashed_password_24', 'president'),
(25, 'jack_roberts_25', 'hashed_password_25', 'president'),
(26, 'emily_wilson_26', 'hashed_password_26', 'president'),
(27, 'michael_jackson_27', 'hashed_password_27', 'president'),
(28, 'sophia_luna_28', 'hashed_password_28', 'president'),
(29, 'olivia_johnson_29', 'hashed_password_29', 'president'),
(30, 'noah_carter_30', 'hashed_password_30', 'president');

update Users
SET user_type= 'member'
WHERE user_id BETWEEN 21 AND 30;

select * from Users


CREATE TABLE TaskAssignment (
    task_id INT PRIMARY KEY,
    member_id INT,
    username NVARCHAR(50) NOT NULL,
    status NVARCHAR(max) DEFAULT 'Not Assigned',
    FOREIGN KEY (member_id) REFERENCES Users(user_id)
);

INSERT INTO TaskAssignment (task_id, member_id, username, status)
VALUES
(11, 21, 'emma_turner_21', 'Not Assigned'),
(12, 22, 'oliver_anderson_22', 'Not Assigned'),
(13, 23, 'liam_smith_23', 'Not Assigned'),
(14, 24, 'sophie_brown_24', 'Not Assigned'),
(15, 25, 'jack_roberts_25', 'Not Assigned'),
(16, 26, 'emily_wilson_26', 'Not Assigned'),
(17, 27, 'michael_jackson_27', 'Not Assigned'),
(18, 28, 'sophia_luna_28', 'Not Assigned'),
(19, 29, 'olivia_johnson_29', 'Not Assigned'),
(20, 30, 'noah_carter_30', 'Not Assigned');


select *  from TaskAssignment


-- Create Events Table
CREATE TABLE PastEvents (
    Pevent_id INT PRIMARY KEY identity(1,1),
    name NVARCHAR(100),
    date DATE,
    society_id INT,
    FOREIGN KEY (society_id) REFERENCES Societies(society_id)
);

select* from PastEvents

select* from events

select*  from Users

create table feedback(
fID INT PRIMARY KEY identity(1,1),
eventName NVARCHAR(100),
societyName NVARCHAR(100),
description NVARCHAR,
Foreign key (societyName) references Societies(name)
)

drop table feedback