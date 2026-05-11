# Intens-test-zadatak
Baza podataka se kreira automatski pri prvom pokretanju sa seed podacima.

Najzahtevniji deo projekta mi je bio rad sa Entity Framework Core-om. Iako sam se upoznao sa njim na fakultetu, nisam imao toliko prilike da ga koristim na projektima jer smo uglavnom podatke čuvali u JSON fajlovima. Bilo mi je zanimljivo da vidim kako rade migracije i kako EF Core mapira C# klase na tabele. Jedna od odluka koju sam doneo bila je da koristim eksplicitnu junction klasu za vezu između kandidata i veština, umesto implicitnog many-to-many, što mi je dalo bolju kontrolu nad operacijama poput uklanjanja veštine od kandidata. Takođe sam se odlučio za SQLite jer ne zahteva setup servera, baza je jedan .db fajl koji ide uz kod.

# Kratko uputstvo
### Kandidati
| Metod | Endpoint | Opis |
|-------|----------|------|
| GET | /api/candidates | Svi kandidati |
| GET | /api/candidates/{id} | Kandidat po ID-u |
| POST | /api/candidates | Dodavanje kandidata |
| PUT | /api/candidates/{id} | Izmena kandidata |
| DELETE | /api/candidates/{id} | Brisanje kandidata |
| POST | /api/candidates/{candidateId}/skills/{skillId} | Dodavanje skilla kandidatu |
| DELETE | /api/candidates/{candidateId}/skills/{skillId} | Uklanjanje skilla od kandidata |
| GET | /api/candidates/search | Pretraga po imenu i/ili skillovima |

### Skillovi
| Metod | Endpoint | Opis |
|-------|----------|------|
| GET | /api/skills | Svi skillovi |
| GET | /api/skills/{id} | Skill po ID-u |
| POST | /api/skills | Dodavanje skilla |
| DELETE | /api/skills/{id} | Brisanje skilla |
