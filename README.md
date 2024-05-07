# Prosjekttittel: 
## ReviewHub

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

### For å bruke nettsiden må du følge disse trinnene:

1. Naviger deg til ReviewHub api mappen og kjør denne kommandoen:
   
Oppdatere databasen med de siste migreringene 
```console
dotnet ef database update
```

### For å tilføre data til databasen, følg disse trinnene nøye:
1. Åpne MySQL Workbench og server og data import.
2. Velg "Import from Self-Contained File" fra verktøylinjen.
3. Angi "review_hub" som standard målskjema og start importprosessen.
4. Etter vellykket import, utfør en omfattende testing av API-et for å sikre at det fungerer i henhold til spesifikasjonene.
5. Installer "Live Server" Extension på Visual Studio Code.
6. Aktiver "index.html" via Live Server for å dynamisk vise nettstedet under utvikling,
