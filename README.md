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

### Validering:

- **FluentValidation.AspNetCore** (Versjon: 11.3.0)

### Logging:

- **Serilog**:
  - Serilog (Versjon: 3.1.1)
  - Serilog.AspNetCore (Versjon: 8.0.1)
  - Serilog.Extensions.Logging (Versjon: 8.0.0)
  - Serilog.Sinks.Console (Versjon: 5.0.1)
  - Serilog.Sinks.File (Versjon: 5.0.0)

## Bruksanvisning

### For å bruke nettsiden må du følge disse trinnene:

1. Naviger deg til ReviewHubAPI-mappen og kjør denne kommandoen:
   
Oppdatere databasen med de siste migreringene 
```console
dotnet ef database update
```

## Funksjoner og Bruk

ReviewHub tilbyr flere kjernefunksjoner som lar brukere interagere med innhold og hverandre på forskjellige måter. Nedenfor er detaljer:

### Legge til Anmeldelser
Brukere kan legge til anmeldelser for filmer gjennom et brukervennlig skjema på nettsiden.
- **API-endepunkt**: POST /api/Review

### Legge til Filmer
Brukere kan legge til detaljer om nye filmer som andre brukere kan anmelde.
- **API-endepunkt**: POST /api/Movie

### Legge til Kommentarer
Brukere kan kommentere på eksisterende anmeldelser for å delta i diskusjoner.
- **API-endepunkt**: POST /api/Comment

### Legge til Movie Posters
Brukere kan laste opp movieposters som gir en visuell representasjon av filmene.
- **API-endepunkt**: POST /api/MoviePoster

### Legge til Profilbilde
Brukere kan laste opp profilbilder som vises på deres brukerprofil.
- **API-endepunkt**: POST /api/uploadprofilepicture/Id={userId}

### Legge til Review Pictures
Brukere kan laste opp bilder som del av deres anmeldelser for å forbedre den visuelle representasjonen.
- **API-endepunkt**: POST /api/ReviewPicture/Id={ReviewId}

### Feilhåndtering
- Systemet logger alle hendelser under prosessen, slik at eventuelle problemer kan diagnostiseres og løses effektivt. Dette inkluderer suksessfulle tillegg av anmeldelser så vel som feil.

Brukere oppfordres til å gi detaljerte og informative anmeldelser, som bidrar til et rikt og engasjerende samfunn på ReviewHub. Hver anmeldelse hjelper andre brukere å gjøre bedre valg om hva de vil se, lese eller strømme.


## Autentisering og Sikkerhet

### Brukerregistrering og Innlogging
- **Brukerregistrering**: Når en ny bruker registrerer seg, sender frontend nødvendig informasjon som brukernavn, 
    e-post og passord til backend  via et HTTPS-kall. Backend vil:
  - Validere innsendt data mot forhåndsdefinerte krav som e-postformat og passordstyrke.
  - Hashe passordet med hashfunksjonen 'bcrypt' før det lagres i databasen for å sikre passordene mot angrep.
  - Opprette en ny brukerrekord i databasen med det hashede passordet og andre relevante brukerdata.
- **Brukerinnlogging**: Ved innlogging sender frontend brukerens påloggingsdetaljer til backend. Backend utfører følgende:
  - Sjekker brukernavnet mot databasen for å hente det hashede passordet.
  - Sammenligner det innsendte passordet med det lagrede hashede passordet.
  - Ved vellykket autentisering, genereres en JWT for brukeren.

### JWT Generering og Håndtering
- **Token Generering**: Ved vellykket autentisering genererer backend en JWT som inkluderer brukeridentifikator og roller som tilskrives brukeren.
- **Token Bruk**: JWT lagres lokalt localStorage og inkluderes i HTTP Authorization-headeren som en 'Bearer' token for hver etterfølgende autentiserte forespørsel.
- **Token Validering**: Backend validerer tokenet ved hver forespørsel, inkludert å sjekke tokenets signatur og utløpstid(per nå satt til 1 time. Denne kan endres, for eksmepel til 1 minutt, i GenerateJwtToken-metoden i AuthService hvis ønskelig for å teste utløpstiden).

### Sikkerhetsmekanismer
- **HTTPS**: All kommunikasjon mellom klient og server skjer over HTTPS.
- **CORS**: Backend aksepterer fores

### For å tilføre data til databasen, følg disse trinnene nøye:
1. Åpne MySQL Workbench
2. Gå til 'server' -> 'data import'.
3. Velg "Import from Self-Contained File" fra verktøylinjen.
4. Angi "review_hub" som standard målskjema og start importprosessen.
5. Velg dumpfilen som ligger i '...\ReviewHubAPI\SQL script\DumpFile\Database 5.sql'.
5. Etter vellykket import, utfør en omfattende testing av API-et for å sikre at det fungerer i henhold til spesifikasjonene.
6. Installer "Live Server" Extension på Visual Studio Code.
7. Aktiver "index.html" på rot-nivå via Live Server for å dynamisk vise nettstedet under utvikling,
