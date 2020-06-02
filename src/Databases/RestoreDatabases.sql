RESTORE DATABASE Announcement FROM DISK = '/var/opt/mssql/backup/Announcement.bak' WITH MOVE 'Announcement' TO '/var/opt/mssql/data/Announcement.mdf', MOVE 'Announcement_log' TO '/var/opt/mssql/data/Announcement_log.ldf';
RESTORE DATABASE [Identity] FROM DISK = '/var/opt/mssql/backup/Identity.bak' WITH MOVE 'Identity' TO '/var/opt/mssql/data/Identity.mdf', MOVE 'Identity_log' TO '/var/opt/mssql/data/Identity_log.ldf';
RESTORE DATABASE Schedule FROM DISK = '/var/opt/mssql/backup/Schedule.bak' WITH MOVE 'Schedule' TO '/var/opt/mssql/data/Schedule.mdf', MOVE 'Schedule_log' TO '/var/opt/mssql/data/Schedule_log.ldf';
RESTORE DATABASE StudyProgress FROM DISK = '/var/opt/mssql/backup/StudyProgress.bak' WITH MOVE 'StudyProgress' TO '/var/opt/mssql/data/StudyProgress.mdf', MOVE 'StudyProgress_log' TO '/var/opt/mssql/data/StudyProgress_log.ldf';