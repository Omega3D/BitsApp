import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Person } from '../_models/person';
import { PersonFileResponse } from '../_models/personfileresponse';
import { PersonRequest } from '../_models/personrequest';

@Injectable({
  providedIn: 'root'
})
export class PersonService {

  constructor(private http: HttpClient) { }

  url: string = 'https://localhost:5001/person';

  getPersons(): Observable<Person[]>{
    return this.http.get<Person[]>(this.url);
  }

  createPersons(file: File): Observable<PersonFileResponse[]> {
    const formData = new FormData();
    formData.append('file', file);

    return this.http.post<PersonFileResponse[]>(this.url, formData); 
  }

  updatePerson(id: number, personRequest: PersonRequest): Observable<number> {
    return this.http.put<number>(`${this.url}/${id}`, personRequest);
  }

  deletePerson(id: number): Observable<number> {
    return this.http.delete<number>(`${this.url}/${id}`);
  }

}
