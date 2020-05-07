import { Injectable } from '@angular/core';
import { HttpClient} from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { catchError, retry } from 'rxjs/operators';
import { Student } from './models/student';

@Injectable()
export class CadillacsServicesService {

  configUrl="http://cadillacs-abp.kn01.fhict.nl/api/students";

  constructor(private http:HttpClient) { }

  getStudent(studenID:number):Observable<Student[]>{
    return this.http.get<Student[]>(this.configUrl+"/"+studenID);
  }

}
