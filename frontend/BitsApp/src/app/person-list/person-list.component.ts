import { Component, OnInit } from '@angular/core';
import { Person } from '../_models/person';
import { PersonService } from '../_services/person.service';
import { PersonRequest } from '../_models/personrequest';
import { PersonFileResponse } from '../_models/personfileresponse';

@Component({
  selector: 'app-person-list',
  templateUrl: './person-list.component.html',
  styleUrls: ['./person-list.component.css']
})
export class PersonListComponent implements OnInit {
  persons: Person[] = [];
  loading: boolean = true;
  error: string | null = null;
  selectedPerson: Person | null = null;
  personRequest: PersonRequest = { name: '', dateOfBirth: new Date(), isMarried: false, phone: '', salary: 0 };
  searchQuery: string = '';
  sortCriteria: 'name' | 'salary' | null = null;


  constructor(private personService: PersonService) {}

  ngOnInit(): void {
    this.loadPersons();
  }

  loadPersons() {
    this.personService.getPersons().subscribe(
      (data: Person[]) => {
        this.persons = data;
        this.loading = false;
      },
      (error) => {
        this.error = 'Failed to load persons';
        this.loading = false;
      }
    );
  }

  createPersons(event: Event) {
    const input = event.target as HTMLInputElement;
    if (input.files && input.files.length > 0) {
        const file = input.files[0];

        // Check if the file is a CSV
        if (file.type !== 'text/csv' && !file.name.endsWith('.csv')) {
            this.error = 'Please upload a valid CSV file.';
            return;
        }

        // Call the service method to upload the CSV file
        this.personService.createPersons(file).subscribe(
            (response: PersonFileResponse[]) => {
                this.loadPersons(); // Reload the persons after upload
            },
            (error) => {
                this.error = 'Failed to upload persons';
            }
        );
    }
  }

  updatePerson(id: number, personRequest: PersonRequest) {
    this.personService.updatePerson(id, personRequest).subscribe(
      () => {
        this.loadPersons();
        this.closeUpdateModal();
      },
      (error) => {
        this.error = 'Failed to update person';
      }
    );
  }

  deletePerson(id: number) {
    this.personService.deletePerson(id).subscribe(
      () => {
        this.loadPersons();
      },
      (error) => {
        this.error = 'Failed to delete person';
      }
    );
  }

  openUpdateModal(person: Person) {
    this.selectedPerson = person;
    this.personRequest = { 
      name: person.name, 
      dateOfBirth: new Date(person.dateOfBirth), 
      isMarried: person.isMarried, 
      phone: person.phone, 
      salary: person.salary 
    };
  }

  closeUpdateModal() {
    this.selectedPerson = null;
    this.personRequest = { name: '', dateOfBirth: new Date(), isMarried: false, phone: '', salary: 0 }; // Reset the request object
  }

  sortPersons(criteria: 'name' | 'salary' | null = this.sortCriteria) {
    if (!criteria) return;

    this.sortCriteria = criteria;

    if (criteria === 'name') {
      this.persons.sort((a, b) => a.name.localeCompare(b.name));
    } else if (criteria === 'salary') {
      this.persons.sort((a, b) => b.salary - a.salary);
    }
  }

}
