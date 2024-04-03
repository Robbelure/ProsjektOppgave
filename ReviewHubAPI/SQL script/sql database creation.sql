
create database review_hub; 

use review_hub; 



DROP USER 'ga-app'@'localhost';
DROP USER 'ga-app'@'%';

CREATE USER IF NOT EXISTS 'ga-app'@'localhost' IDENTIFIED BY 'pass123';
CREATE USER IF NOT EXISTS 'ga-app'@'%' IDENTIFIED BY 'pass123';

GRANT ALL PRIVILEGES ON review_hub.* TO 'ga-app'@'%';
GRANT ALL PRIVILEGES ON review_hub.* TO 'ga-app'@'localhost';