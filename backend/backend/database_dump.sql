PRAGMA foreign_keys=OFF;
BEGIN TRANSACTION;
CREATE TABLE IF NOT EXISTS "Felhasznalok" (
    "Id" INTEGER NOT NULL CONSTRAINT "PK_Felhasznalok" PRIMARY KEY AUTOINCREMENT,
    "Nev" TEXT NOT NULL,
    "Email" TEXT NOT NULL,
    "Jelszo" TEXT NOT NULL,
    "IsAdmin" INTEGER NOT NULL
);
INSERT INTO Felhasznalok VALUES(1,'Admin','admin@admin.com','$2a$11$Ra3SzSNoE9wj/udSkVrUHeF0IBMiTdwLBOeE6nHzriJAO0k5Pn71i',1);
INSERT INTO Felhasznalok VALUES(2,'Altaisss','szabo.akornel@gmail.com','$2a$11$k/2KmVuQXJFW5qIMxosHkeW662VAfR3x6Gq0IZfNwmCFfOfKiqx2O',0);
CREATE TABLE IF NOT EXISTS "Filmek" (
    "Id" INTEGER NOT NULL CONSTRAINT "PK_Filmek" PRIMARY KEY AUTOINCREMENT,
    "Hossz" INTEGER NOT NULL,
    "Leiras" TEXT NULL,
    "Cim" TEXT NOT NULL,
    "Rendezo" TEXT NOT NULL,
    "KepUrl" TEXT NULL
);
INSERT INTO Filmek VALUES(1,99,'Two neighbors form a strong bond after both suspect extramarital activities of their spouses. However, they agree to keep their bond platonic so as not to commit similar wrongs.','In the Mood for Love','Wong Kar-Wai','/Images/6774a6ba-dd3e-409f-a0b2-03924122e06e.jpg');
INSERT INTO Filmek VALUES(2,124,'A secret military project endangers Neo-Tokyo when it turns a biker gang member into a rampaging psychic psychopath that only two teenagers and a group of psychics can stop.','Akira','Katsuhiro Otomo','/Images/93412242-6dc2-4fb6-9060-241f259b1eab.jpg');
INSERT INTO Filmek VALUES(3,117,'During its return to the earth, commercial spaceship Nostromo intercepts a distress signal from a distant planet. When a three-member team of the crew discovers a chamber containing thousands of eggs on the planet, a creature inside one of the eggs attacks an explorer. The entire crew is unaware of the impending nightmare set to descend upon them when the alien parasite planted inside its unfortunate host is birthed.','Alien','Ridley Scott','/Images/d9156bcf-1502-4a23-89bd-c26002241369.jpg');
INSERT INTO Filmek VALUES(4,155,'A monumental windstorm and an abused horse’s refusal to work or eat signal the beginning of the end for a poor farmer and his daughter.','A torinói ló','Béla Tarr','/Images/8121a572-dc3d-47cc-b17f-209ac0ad0047.jpg');
INSERT INTO Filmek VALUES(5,117,'After a global war, the seaside kingdom known as the Valley of the Wind remains one of the last strongholds on Earth untouched by a poisonous jungle and the powerful insects that guard it. Led by the courageous Princess Nausicaä, the people of the Valley engage in an epic struggle to restore the bond between humanity and Earth.','Nausicaä of the Valley of the Wind','Hayao Miyazaki','/Images/0f460908-d5e2-4fba-9ff7-554ea6a644b5.jpg');
INSERT INTO Filmek VALUES(6,98,'After a chaotic night of rioting in a marginal suburb of Paris, three young friends, Vinz, Hubert and Saïd, wander around unoccupied waiting for news about the state of health of a mutual friend who has been seriously injured when confronting the police.','La Haine','Mathieu Kassovitz','/Images/45c075a3-f8a8-42b7-8949-24c36897d6b5.jpg');
INSERT INTO Filmek VALUES(7,95,'After the insane General Jack D. Ripper initiates a nuclear strike on the Soviet Union, a war room full of politicians, generals and a Russian diplomat all frantically try to stop it.','Dr. Strangelove or: How I Learned to Stop Worrying and Love the Bomb','Stanley Kubrick','/Images/5374c543-013b-4cb9-883e-7de8f66669c9.jpg');
INSERT INTO Filmek VALUES(8,162,'Near a gray and unnamed city is the Zone, a place guarded by barbed wire and soldiers, and where the normal laws of physics are victim to frequent anomalies. A stalker guides two men into the Zone, specifically to an area in which deep-seated desires are granted.','Stalker','Andrei Tarkovsky','/Images/65ede8e1-b365-4316-ad76-4d14186391df.jpg');
INSERT INTO Filmek VALUES(9,143,'Kanji Watanabe is a middle-aged man who has worked in the same monotonous bureaucratic position for decades. Learning he has cancer, he starts to look for the meaning of his life.','Ikiru','Akira Kurosawa','/Images/53932456-960f-44f4-9a21-952a7f873d89.jpg');
INSERT INTO Filmek VALUES(10,135,'Down-on-his-luck veteran Tsugumo Hanshirō enters the courtyard of the prosperous House of Iyi. Unemployed, and with no family, he hopes to find a place to commit seppuku—and a worthy second to deliver the coup de grâce in his suicide ritual. The senior counselor for the Iyi clan questions the ronin’s resolve and integrity, suspecting Hanshirō of seeking charity rather than an honorable end. What follows is a pair of interlocking stories which lay bare the difference between honor and respect, and promises to examine the legendary foundations of the Samurai code.','Harakiri','Masaki Kobayashi','/Images/72b44461-f524-4185-a37d-d74902b1520d.jpg');
INSERT INTO Filmek VALUES(12,111,'A wave of gruesome murders sweeps Tokyo, and the only link is a bloody X carved into the victims’ necks. In each case, the murderer is found nearby and recalls nothing of the crime. Detective Takabe and psychologist Sakuma are called in to figure out the connection, but their investigation goes nowhere. An odd young man, arrested near the latest murder, has a strange effect on everyone with whom he comes into contact. Takabe starts a series of interrogations to determine the man’s connection with the killings.','Cure','Kiyoshi Kurosawa','/Images/8c0f1f39-d85c-4880-a3ee-28d31a3c76e3.jpg');
CREATE TABLE IF NOT EXISTS "Termek" (
    "Id" INTEGER NOT NULL CONSTRAINT "PK_Termek" PRIMARY KEY AUTOINCREMENT,
    "TeremNev" TEXT NOT NULL
);
INSERT INTO Termek VALUES(1,'1. Terem');
INSERT INTO Termek VALUES(2,'2. Terem');
INSERT INTO Termek VALUES(3,'3. Terem');
CREATE TABLE IF NOT EXISTS "Foglalasok" (
    "Id" INTEGER NOT NULL CONSTRAINT "PK_Foglalasok" PRIMARY KEY AUTOINCREMENT,
    "FelhasznaloId" INTEGER NOT NULL,
    CONSTRAINT "FK_Foglalasok_Felhasznalok_FelhasznaloId" FOREIGN KEY ("FelhasznaloId") REFERENCES "Felhasznalok" ("Id") ON DELETE RESTRICT
);
INSERT INTO Foglalasok VALUES(2,2);
CREATE TABLE IF NOT EXISTS "Szekek" (
    "Id" INTEGER NOT NULL CONSTRAINT "PK_Szekek" PRIMARY KEY AUTOINCREMENT,
    "Sor" INTEGER NOT NULL,
    "Szam" INTEGER NOT NULL,
    "Oldal" TEXT NOT NULL,
    "TeremId" INTEGER NOT NULL,
    CONSTRAINT "FK_Szekek_Termek_TeremId" FOREIGN KEY ("TeremId") REFERENCES "Termek" ("Id") ON DELETE RESTRICT
);
INSERT INTO Szekek VALUES(1,1,1,'B',1);
INSERT INTO Szekek VALUES(2,1,2,'B',1);
INSERT INTO Szekek VALUES(3,1,3,'B',1);
INSERT INTO Szekek VALUES(4,1,4,'B',1);
INSERT INTO Szekek VALUES(5,1,5,'B',1);
INSERT INTO Szekek VALUES(6,1,6,'J',1);
INSERT INTO Szekek VALUES(7,1,7,'J',1);
INSERT INTO Szekek VALUES(8,1,8,'J',1);
INSERT INTO Szekek VALUES(9,1,9,'J',1);
INSERT INTO Szekek VALUES(10,1,10,'J',1);
INSERT INTO Szekek VALUES(11,2,1,'B',1);
INSERT INTO Szekek VALUES(12,2,2,'B',1);
INSERT INTO Szekek VALUES(13,2,3,'B',1);
INSERT INTO Szekek VALUES(14,2,4,'B',1);
INSERT INTO Szekek VALUES(15,2,5,'B',1);
INSERT INTO Szekek VALUES(16,2,6,'J',1);
INSERT INTO Szekek VALUES(17,2,7,'J',1);
INSERT INTO Szekek VALUES(18,2,8,'J',1);
INSERT INTO Szekek VALUES(19,2,9,'J',1);
INSERT INTO Szekek VALUES(20,2,10,'J',1);
INSERT INTO Szekek VALUES(21,3,1,'B',1);
INSERT INTO Szekek VALUES(22,3,2,'B',1);
INSERT INTO Szekek VALUES(23,3,3,'B',1);
INSERT INTO Szekek VALUES(24,3,4,'B',1);
INSERT INTO Szekek VALUES(25,3,5,'B',1);
INSERT INTO Szekek VALUES(26,3,6,'J',1);
INSERT INTO Szekek VALUES(27,3,7,'J',1);
INSERT INTO Szekek VALUES(28,3,8,'J',1);
INSERT INTO Szekek VALUES(29,3,9,'J',1);
INSERT INTO Szekek VALUES(30,3,10,'J',1);
INSERT INTO Szekek VALUES(31,4,1,'B',1);
INSERT INTO Szekek VALUES(32,4,2,'B',1);
INSERT INTO Szekek VALUES(33,4,3,'B',1);
INSERT INTO Szekek VALUES(34,4,4,'B',1);
INSERT INTO Szekek VALUES(35,4,5,'B',1);
INSERT INTO Szekek VALUES(36,4,6,'J',1);
INSERT INTO Szekek VALUES(37,4,7,'J',1);
INSERT INTO Szekek VALUES(38,4,8,'J',1);
INSERT INTO Szekek VALUES(39,4,9,'J',1);
INSERT INTO Szekek VALUES(40,4,10,'J',1);
INSERT INTO Szekek VALUES(41,5,1,'B',1);
INSERT INTO Szekek VALUES(42,5,2,'B',1);
INSERT INTO Szekek VALUES(43,5,3,'B',1);
INSERT INTO Szekek VALUES(44,5,4,'B',1);
INSERT INTO Szekek VALUES(45,5,5,'B',1);
INSERT INTO Szekek VALUES(46,5,6,'J',1);
INSERT INTO Szekek VALUES(47,5,7,'J',1);
INSERT INTO Szekek VALUES(48,5,8,'J',1);
INSERT INTO Szekek VALUES(49,5,9,'J',1);
INSERT INTO Szekek VALUES(50,5,10,'J',1);
INSERT INTO Szekek VALUES(51,6,1,'B',1);
INSERT INTO Szekek VALUES(52,6,2,'B',1);
INSERT INTO Szekek VALUES(53,6,3,'B',1);
INSERT INTO Szekek VALUES(54,6,4,'B',1);
INSERT INTO Szekek VALUES(55,6,5,'B',1);
INSERT INTO Szekek VALUES(56,6,6,'J',1);
INSERT INTO Szekek VALUES(57,6,7,'J',1);
INSERT INTO Szekek VALUES(58,6,8,'J',1);
INSERT INTO Szekek VALUES(59,6,9,'J',1);
INSERT INTO Szekek VALUES(60,6,10,'J',1);
INSERT INTO Szekek VALUES(61,7,1,'B',1);
INSERT INTO Szekek VALUES(62,7,2,'B',1);
INSERT INTO Szekek VALUES(63,7,3,'B',1);
INSERT INTO Szekek VALUES(64,7,4,'B',1);
INSERT INTO Szekek VALUES(65,7,5,'B',1);
INSERT INTO Szekek VALUES(66,7,6,'J',1);
INSERT INTO Szekek VALUES(67,7,7,'J',1);
INSERT INTO Szekek VALUES(68,7,8,'J',1);
INSERT INTO Szekek VALUES(69,7,9,'J',1);
INSERT INTO Szekek VALUES(70,7,10,'J',1);
INSERT INTO Szekek VALUES(71,8,1,'B',1);
INSERT INTO Szekek VALUES(72,8,2,'B',1);
INSERT INTO Szekek VALUES(73,8,3,'B',1);
INSERT INTO Szekek VALUES(74,8,4,'B',1);
INSERT INTO Szekek VALUES(75,8,5,'B',1);
INSERT INTO Szekek VALUES(76,8,6,'J',1);
INSERT INTO Szekek VALUES(77,8,7,'J',1);
INSERT INTO Szekek VALUES(78,8,8,'J',1);
INSERT INTO Szekek VALUES(79,8,9,'J',1);
INSERT INTO Szekek VALUES(80,8,10,'J',1);
INSERT INTO Szekek VALUES(81,1,1,'B',2);
INSERT INTO Szekek VALUES(82,1,2,'B',2);
INSERT INTO Szekek VALUES(83,1,3,'B',2);
INSERT INTO Szekek VALUES(84,1,4,'B',2);
INSERT INTO Szekek VALUES(85,1,5,'J',2);
INSERT INTO Szekek VALUES(86,1,6,'J',2);
INSERT INTO Szekek VALUES(87,1,7,'J',2);
INSERT INTO Szekek VALUES(88,1,8,'J',2);
INSERT INTO Szekek VALUES(89,2,1,'B',2);
INSERT INTO Szekek VALUES(90,2,2,'B',2);
INSERT INTO Szekek VALUES(91,2,3,'B',2);
INSERT INTO Szekek VALUES(92,2,4,'B',2);
INSERT INTO Szekek VALUES(93,2,5,'J',2);
INSERT INTO Szekek VALUES(94,2,6,'J',2);
INSERT INTO Szekek VALUES(95,2,7,'J',2);
INSERT INTO Szekek VALUES(96,2,8,'J',2);
INSERT INTO Szekek VALUES(97,3,1,'B',2);
INSERT INTO Szekek VALUES(98,3,2,'B',2);
INSERT INTO Szekek VALUES(99,3,3,'B',2);
INSERT INTO Szekek VALUES(100,3,4,'B',2);
INSERT INTO Szekek VALUES(101,3,5,'J',2);
INSERT INTO Szekek VALUES(102,3,6,'J',2);
INSERT INTO Szekek VALUES(103,3,7,'J',2);
INSERT INTO Szekek VALUES(104,3,8,'J',2);
INSERT INTO Szekek VALUES(105,4,1,'B',2);
INSERT INTO Szekek VALUES(106,4,2,'B',2);
INSERT INTO Szekek VALUES(107,4,3,'B',2);
INSERT INTO Szekek VALUES(108,4,4,'B',2);
INSERT INTO Szekek VALUES(109,4,5,'J',2);
INSERT INTO Szekek VALUES(110,4,6,'J',2);
INSERT INTO Szekek VALUES(111,4,7,'J',2);
INSERT INTO Szekek VALUES(112,4,8,'J',2);
INSERT INTO Szekek VALUES(113,5,1,'B',2);
INSERT INTO Szekek VALUES(114,5,2,'B',2);
INSERT INTO Szekek VALUES(115,5,3,'B',2);
INSERT INTO Szekek VALUES(116,5,4,'B',2);
INSERT INTO Szekek VALUES(117,5,5,'J',2);
INSERT INTO Szekek VALUES(118,5,6,'J',2);
INSERT INTO Szekek VALUES(119,5,7,'J',2);
INSERT INTO Szekek VALUES(120,5,8,'J',2);
INSERT INTO Szekek VALUES(121,6,1,'B',2);
INSERT INTO Szekek VALUES(122,6,2,'B',2);
INSERT INTO Szekek VALUES(123,6,3,'B',2);
INSERT INTO Szekek VALUES(124,6,4,'B',2);
INSERT INTO Szekek VALUES(125,6,5,'J',2);
INSERT INTO Szekek VALUES(126,6,6,'J',2);
INSERT INTO Szekek VALUES(127,6,7,'J',2);
INSERT INTO Szekek VALUES(128,6,8,'J',2);
INSERT INTO Szekek VALUES(129,7,1,'B',2);
INSERT INTO Szekek VALUES(130,7,2,'B',2);
INSERT INTO Szekek VALUES(131,7,3,'B',2);
INSERT INTO Szekek VALUES(132,7,4,'B',2);
INSERT INTO Szekek VALUES(133,7,5,'J',2);
INSERT INTO Szekek VALUES(134,7,6,'J',2);
INSERT INTO Szekek VALUES(135,7,7,'J',2);
INSERT INTO Szekek VALUES(136,7,8,'J',2);
INSERT INTO Szekek VALUES(137,8,1,'B',2);
INSERT INTO Szekek VALUES(138,8,2,'B',2);
INSERT INTO Szekek VALUES(139,8,3,'B',2);
INSERT INTO Szekek VALUES(140,8,4,'B',2);
INSERT INTO Szekek VALUES(141,8,5,'J',2);
INSERT INTO Szekek VALUES(142,8,6,'J',2);
INSERT INTO Szekek VALUES(143,8,7,'J',2);
INSERT INTO Szekek VALUES(144,8,8,'J',2);
INSERT INTO Szekek VALUES(145,1,1,'B',3);
INSERT INTO Szekek VALUES(146,1,2,'B',3);
INSERT INTO Szekek VALUES(147,1,3,'B',3);
INSERT INTO Szekek VALUES(148,1,4,'B',3);
INSERT INTO Szekek VALUES(149,1,5,'J',3);
INSERT INTO Szekek VALUES(150,1,6,'J',3);
INSERT INTO Szekek VALUES(151,1,7,'J',3);
INSERT INTO Szekek VALUES(152,1,8,'J',3);
INSERT INTO Szekek VALUES(153,2,1,'B',3);
INSERT INTO Szekek VALUES(154,2,2,'B',3);
INSERT INTO Szekek VALUES(155,2,3,'B',3);
INSERT INTO Szekek VALUES(156,2,4,'B',3);
INSERT INTO Szekek VALUES(157,2,5,'J',3);
INSERT INTO Szekek VALUES(158,2,6,'J',3);
INSERT INTO Szekek VALUES(159,2,7,'J',3);
INSERT INTO Szekek VALUES(160,2,8,'J',3);
INSERT INTO Szekek VALUES(161,3,1,'B',3);
INSERT INTO Szekek VALUES(162,3,2,'B',3);
INSERT INTO Szekek VALUES(163,3,3,'B',3);
INSERT INTO Szekek VALUES(164,3,4,'B',3);
INSERT INTO Szekek VALUES(165,3,5,'J',3);
INSERT INTO Szekek VALUES(166,3,6,'J',3);
INSERT INTO Szekek VALUES(167,3,7,'J',3);
INSERT INTO Szekek VALUES(168,3,8,'J',3);
INSERT INTO Szekek VALUES(169,4,1,'B',3);
INSERT INTO Szekek VALUES(170,4,2,'B',3);
INSERT INTO Szekek VALUES(171,4,3,'B',3);
INSERT INTO Szekek VALUES(172,4,4,'B',3);
INSERT INTO Szekek VALUES(173,4,5,'J',3);
INSERT INTO Szekek VALUES(174,4,6,'J',3);
INSERT INTO Szekek VALUES(175,4,7,'J',3);
INSERT INTO Szekek VALUES(176,4,8,'J',3);
INSERT INTO Szekek VALUES(177,5,1,'B',3);
INSERT INTO Szekek VALUES(178,5,2,'B',3);
INSERT INTO Szekek VALUES(179,5,3,'B',3);
INSERT INTO Szekek VALUES(180,5,4,'B',3);
INSERT INTO Szekek VALUES(181,5,5,'J',3);
INSERT INTO Szekek VALUES(182,5,6,'J',3);
INSERT INTO Szekek VALUES(183,5,7,'J',3);
INSERT INTO Szekek VALUES(184,5,8,'J',3);
INSERT INTO Szekek VALUES(185,6,1,'B',3);
INSERT INTO Szekek VALUES(186,6,2,'B',3);
INSERT INTO Szekek VALUES(187,6,3,'B',3);
INSERT INTO Szekek VALUES(188,6,4,'B',3);
INSERT INTO Szekek VALUES(189,6,5,'J',3);
INSERT INTO Szekek VALUES(190,6,6,'J',3);
INSERT INTO Szekek VALUES(191,6,7,'J',3);
INSERT INTO Szekek VALUES(192,6,8,'J',3);
CREATE TABLE IF NOT EXISTS "Vetitesek" (
    "Id" INTEGER NOT NULL CONSTRAINT "PK_Vetitesek" PRIMARY KEY AUTOINCREMENT,
    "Idopont" TEXT NOT NULL,
    "TeremId" INTEGER NOT NULL,
    "FilmId" INTEGER NOT NULL,
    "JegyAr" INTEGER NOT NULL,
    "Nyelv" TEXT NULL,
    CONSTRAINT "FK_Vetitesek_Filmek_FilmId" FOREIGN KEY ("FilmId") REFERENCES "Filmek" ("Id") ON DELETE RESTRICT,
    CONSTRAINT "FK_Vetitesek_Termek_TeremId" FOREIGN KEY ("TeremId") REFERENCES "Termek" ("Id") ON DELETE RESTRICT
);
INSERT INTO Vetitesek VALUES(18,'2026-04-12 10:00:00',1,1,2000,'Eredeti');
INSERT INTO Vetitesek VALUES(19,'2026-04-13 16:00:00',1,1,2500,'Eredeti');
INSERT INTO Vetitesek VALUES(20,'2026-04-14 19:00:00',1,1,1800,'Magyar');
INSERT INTO Vetitesek VALUES(21,'2026-04-15 19:00:00',3,1,1800,'Magyar');
INSERT INTO Vetitesek VALUES(22,'2026-04-16 10:00:00',1,1,2200,'Magyar');
INSERT INTO Vetitesek VALUES(23,'2026-04-18 10:00:00',1,1,1800,'Magyar');
INSERT INTO Vetitesek VALUES(24,'2026-04-12 19:00:00',1,2,1800,'Eredeti');
INSERT INTO Vetitesek VALUES(25,'2026-04-13 19:00:00',1,2,2000,'Magyar');
INSERT INTO Vetitesek VALUES(26,'2026-04-15 13:00:00',1,2,2200,'Magyar');
INSERT INTO Vetitesek VALUES(27,'2026-04-16 16:00:00',3,2,2000,'Szinkronizált');
INSERT INTO Vetitesek VALUES(28,'2026-04-17 13:00:00',2,2,2200,'Szinkronizált');
INSERT INTO Vetitesek VALUES(29,'2026-04-12 19:00:00',3,3,2500,'Magyar');
INSERT INTO Vetitesek VALUES(30,'2026-04-15 16:00:00',1,3,2500,'Szinkronizált');
INSERT INTO Vetitesek VALUES(31,'2026-04-16 13:00:00',1,3,1800,'Magyar');
INSERT INTO Vetitesek VALUES(32,'2026-04-18 19:00:00',2,3,2500,'Magyar');
INSERT INTO Vetitesek VALUES(33,'2026-04-12 10:00:00',2,4,1800,'Magyar');
INSERT INTO Vetitesek VALUES(34,'2026-04-13 13:00:00',1,4,1800,'Szinkronizált');
INSERT INTO Vetitesek VALUES(35,'2026-04-15 10:00:00',3,4,2500,'Szinkronizált');
INSERT INTO Vetitesek VALUES(36,'2026-04-17 13:00:00',1,4,1800,'Magyar');
INSERT INTO Vetitesek VALUES(37,'2026-04-13 16:00:00',3,5,1800,'Eredeti');
INSERT INTO Vetitesek VALUES(38,'2026-04-14 13:00:00',3,5,1800,'Eredeti');
INSERT INTO Vetitesek VALUES(39,'2026-04-15 13:00:00',3,5,2000,'Magyar');
INSERT INTO Vetitesek VALUES(40,'2026-04-18 13:00:00',2,5,2200,'Szinkronizált');
INSERT INTO Vetitesek VALUES(41,'2026-04-13 13:00:00',2,6,2500,'Magyar');
INSERT INTO Vetitesek VALUES(42,'2026-04-14 19:00:00',3,6,2500,'Magyar');
INSERT INTO Vetitesek VALUES(43,'2026-04-16 10:00:00',3,6,2200,'Eredeti');
INSERT INTO Vetitesek VALUES(44,'2026-04-17 16:00:00',2,6,1800,'Magyar');
INSERT INTO Vetitesek VALUES(45,'2026-04-18 10:00:00',2,6,1800,'Magyar');
INSERT INTO Vetitesek VALUES(46,'2026-04-12 16:00:00',2,7,2200,'Eredeti');
INSERT INTO Vetitesek VALUES(47,'2026-04-14 16:00:00',2,7,2200,'Szinkronizált');
INSERT INTO Vetitesek VALUES(48,'2026-04-16 13:00:00',2,7,2500,'Magyar');
INSERT INTO Vetitesek VALUES(49,'2026-04-17 16:00:00',3,7,2000,'Magyar');
INSERT INTO Vetitesek VALUES(50,'2026-04-18 10:00:00',3,7,2500,'Szinkronizált');
INSERT INTO Vetitesek VALUES(51,'2026-04-12 19:00:00',2,8,1800,'Eredeti');
INSERT INTO Vetitesek VALUES(52,'2026-04-15 16:00:00',3,8,2500,'Szinkronizált');
INSERT INTO Vetitesek VALUES(53,'2026-04-16 10:00:00',2,8,1800,'Magyar');
INSERT INTO Vetitesek VALUES(54,'2026-04-17 10:00:00',2,8,2200,'Szinkronizált');
INSERT INTO Vetitesek VALUES(55,'2026-04-18 19:00:00',1,8,2200,'Magyar');
INSERT INTO Vetitesek VALUES(56,'2026-04-12 10:00:00',3,9,2500,'Szinkronizált');
INSERT INTO Vetitesek VALUES(57,'2026-04-13 10:00:00',1,9,1800,'Szinkronizált');
INSERT INTO Vetitesek VALUES(58,'2026-04-14 16:00:00',3,9,1800,'Szinkronizált');
INSERT INTO Vetitesek VALUES(59,'2026-04-15 10:00:00',1,9,2000,'Szinkronizált');
INSERT INTO Vetitesek VALUES(60,'2026-04-17 10:00:00',1,9,2500,'Szinkronizált');
INSERT INTO Vetitesek VALUES(61,'2026-04-18 16:00:00',2,9,2000,'Szinkronizált');
INSERT INTO Vetitesek VALUES(62,'2026-04-13 19:00:00',2,10,1800,'Eredeti');
INSERT INTO Vetitesek VALUES(63,'2026-04-14 13:00:00',2,10,2000,'Szinkronizált');
INSERT INTO Vetitesek VALUES(64,'2026-04-15 19:00:00',2,10,1800,'Szinkronizált');
INSERT INTO Vetitesek VALUES(65,'2026-04-16 16:00:00',1,10,1800,'Eredeti');
INSERT INTO Vetitesek VALUES(66,'2026-04-17 19:00:00',1,10,2200,'Eredeti');
INSERT INTO Vetitesek VALUES(67,'2026-04-18 19:00:00',3,10,2200,'Magyar');
INSERT INTO Vetitesek VALUES(68,'2026-04-13 21:00:00',3,12,2300,'Japán');
INSERT INTO Vetitesek VALUES(69,'2026-04-15 17:00:00',2,12,2300,'Japán');
INSERT INTO Vetitesek VALUES(70,'2026-04-17 21:00:00',3,12,2300,'Japán');
INSERT INTO Vetitesek VALUES(71,'2026-04-18 22:00:00',2,12,2300,'Japán');
INSERT INTO Vetitesek VALUES(72,'2026-04-14 15:00:00',1,12,2300,'Japán');
CREATE TABLE IF NOT EXISTS "Foglalthelyek" (
    "Id" INTEGER NOT NULL CONSTRAINT "PK_Foglalthelyek" PRIMARY KEY AUTOINCREMENT,
    "SzekId" INTEGER NOT NULL,
    "FoglalasId" INTEGER NOT NULL,
    "VetitesId" INTEGER NOT NULL,
    CONSTRAINT "FK_Foglalthelyek_Foglalasok_FoglalasId" FOREIGN KEY ("FoglalasId") REFERENCES "Foglalasok" ("Id") ON DELETE CASCADE,
    CONSTRAINT "FK_Foglalthelyek_Szekek_SzekId" FOREIGN KEY ("SzekId") REFERENCES "Szekek" ("Id") ON DELETE RESTRICT,
    CONSTRAINT "FK_Foglalthelyek_Vetitesek_VetitesId" FOREIGN KEY ("VetitesId") REFERENCES "Vetitesek" ("Id") ON DELETE RESTRICT
);
INSERT INTO Foglalthelyek VALUES(3,56,2,66);
PRAGMA writable_schema=ON;
CREATE TABLE IF NOT EXISTS sqlite_sequence(name,seq);
DELETE FROM sqlite_sequence;
INSERT INTO sqlite_sequence VALUES('Termek',3);
INSERT INTO sqlite_sequence VALUES('Szekek',192);
INSERT INTO sqlite_sequence VALUES('Felhasznalok',2);
INSERT INTO sqlite_sequence VALUES('Filmek',12);
INSERT INTO sqlite_sequence VALUES('Vetitesek',72);
INSERT INTO sqlite_sequence VALUES('Foglalasok',2);
INSERT INTO sqlite_sequence VALUES('Foglalthelyek',3);
CREATE UNIQUE INDEX "IX_Felhasznalo_Email_Unique" ON "Felhasznalok" ("Email");
CREATE INDEX "IX_Film_Cim" ON "Filmek" ("Cim");
CREATE INDEX "IX_Foglalas_FelhasznaloId" ON "Foglalasok" ("FelhasznaloId");
CREATE UNIQUE INDEX "IX_Foglalthely_Vetites_Szek_Unique" ON "Foglalthelyek" ("VetitesId", "SzekId");
CREATE INDEX "IX_Foglalthelyek_FoglalasId" ON "Foglalthelyek" ("FoglalasId");
CREATE INDEX "IX_Foglalthelyek_SzekId" ON "Foglalthelyek" ("SzekId");
CREATE INDEX "IX_Szekek_TeremId" ON "Szekek" ("TeremId");
CREATE INDEX "IX_Vetites_FilmId_Idopont" ON "Vetitesek" ("FilmId", "Idopont");
CREATE INDEX "IX_Vetites_Idopont" ON "Vetitesek" ("Idopont");
CREATE INDEX "IX_Vetites_TeremId_Idopont" ON "Vetitesek" ("TeremId", "Idopont");
PRAGMA writable_schema=OFF;
COMMIT;
