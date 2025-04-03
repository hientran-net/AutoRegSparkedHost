CREATE TABLE DangKi(
	Id INTEGER PRIMARY KEY AUTOINCREMENT,
	Firstname TEXT,
	Lastname TEXT,
	Emailadress TEXT NOT NULL,
	Phonenumber TEXT NOT NULL,
	Companyname TEXT,
	Streetadress1 TEXT,
	Streetadress2 TEXT,
	City TEXT,
	Postcode TEXT,
	TaxID TEXT,
	Password TEXT  NOT NULL,
	Ipaddress TEXT NOT NULL,
	Add_date TEXT DEFAULT (DATETIME('now', '+7 hours'))
);

DROP TABLE DangKi

INSERT INTO DangKi (Emailadress, Password, Phonenumber, Ipaddress) VALUES
('buihieu7949@gmail.com', 'xaF{QchNpXa:', '0973513097', '51.79.230.157:27042'),
('vuhoa0510@gmail.com', '|ycn3%1p:.wh', '0355557755', '51.79.230.157:27042'),
('buihuong3429@gmail.com', 'd9sl#-)oEzgz', '0819811849', '51.79.230.157:27042'),
('trannam7709@gmail.com', 'LhgtQQV%Q8Jt', '0381092801', '51.79.230.157:27042'),
('phannam7326@gmail.com', 'BSXq,kqkn+Du', '0365459504', '51.79.230.157:27042'),
('dothuy7778@gmail.com', 'HjJzMckudvu0', '0322057245', '51.79.230.157:27042'),
('tranhieu4700@gmail.com', 's)1+QR6QgMU,', '0823502381', '51.79.230.157:27042'),
('buithuy2197@gmail.com', 'YG1Zctp~@Ln{', '0761385889', '15.235.144.149:27036'),
('vuthuy7252@gmail.com', 'IjqPq9_)QM!s', '0924988232', '15.235.144.149:27036'),
('lethuy3081@gmail.com', 'x;|RBee4FSl*', '0917843636', '15.235.144.149:27036'),
('phamthanh4635@gmail.com', '~%@iSOXv_;m,', '0324340961', '15.235.144.149:27036'),
('dominh6783@gmail.com', '(|5C#4Xjf*Y6', '0825489196', '15.235.144.149:27036'),
('hoangduc4258@gmail.com', 'F+QR0jZu$j~$', '0968955959', '15.235.144.149:27036'),
('phamthuy9066@gmail.com', '+bAL0dSdXgkT', '0983319945', '15.235.144.149:27036'),
('buihuong9137@gmail.com', 'qhg,Zg%6=0sq', '0560014929', '15.235.144.149:27036'),
('lenam7106@gmail.com', '=jU%Q-8jc1,X', '0787847917', '15.235.144.149:27036'),
('hoangthuy1554@gmail.com', 'E5S@F|sI;F+V', '0841592711', '15.235.144.149:27036');


