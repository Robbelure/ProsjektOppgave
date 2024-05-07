# Prosjekttittel: 
ReviewHub

# Prosjektbeskrivelse:
ReviewHub er en nettside hvor brukerne kan skrive anmeldelser av filmer, bøker og TV-serier. 
Brukerne kan også kommentere på andres anmeldelser. Med ReviewHub kan brukere lese hva andre synes om ulike medieformater. 
I tillegg til å lese andres anmeldelser, kan brukerne også skrive sine egne. Dette skaper et aktivt miljø hvor folk kan diskutere og utveksle meninger om ulike medietilbud.
ReviewHub er ikke bare et sted for anmeldelser, men også et fellesskap av medieinteresserte som deler sine synspunkter. Dette hjelper andre med å ta informerte valg om hva de vil se, lese eller strømme.

# Pakker og Avhengigheter

For å bygge og kjøre dette prosjektet, må følgende pakker inkluderes:

### Autentisering, Autorisasjon og Kryptering:

- **Microsoft.AspNetCore.Authentication.JwtBearer** (Versjon: 8.0.2)
- **BCrypt.Net-Next** (Versjon: 4.0.3)

### API Dokumentasjon og Swagger-grensesnitt:

- **Microsoft.AspNetCore.OpenApi** (Versjon: 8.0.2)
- **Swashbuckle.AspNetCore** (Versjon: 6.5.0)

### Database og Entity Framework:

- **Microsoft.EntityFrameworkCore** (Versjon: 8.0.2)
- **Microsoft.EntityFrameworkCore.Tools** (Versjon: 8.0.2)
- **Pomelo.EntityFrameworkCore.MySql** (Versjon: 8.0.1)

### Logging:

- **Serilog**:
  - Serilog (Versjon: 3.1.1)
  - Serilog.AspNetCore (Versjon: 8.0.1)
  - Serilog.Extensions.Logging (Versjon: 8.0.0)
  - Serilog.Sinks.Console (Versjon: 5.0.1)
  - Serilog.Sinks.File (Versjon: 5.0.0)

## Bruksanvisning

For å bruke nettsiden må du følge disse trinnene:

1. Legge til en ny migrering  ved å bruke følgende kommandoer:
```console
dotnet ef migrations add [MigrationsName] -o Data/Migrations
```
Oppdatere databasen med de siste migreringene 
```console
dotnet ef database update
```

2. For å fylle databasen med data, kjør filen `Database 4.sql` som finnes i mappen `ReviewHubAPI/SQL script/DumpFile`. Denne filen vil fylle inn brukerdata, filmdata og kommentarer i databasen din.

3. Kjør API-et og bekreft at det kjører som forventet.

4. Installer "Live Server" Extension på Visual Studio Code.

5. Kjør `index.html` via Live Server for å bruke nettsiden.
