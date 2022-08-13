sc create TravelHub_DbDataService binPath= %~dp0DbData.Service.exe
sc failure TravelHub_DbDataService actions= restart/60000/restart/60000/""/60000 reset= 86400
sc start TravelHub_DbDataService
sc config TravelHub_DbDataService start=auto