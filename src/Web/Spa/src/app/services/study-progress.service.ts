import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Student } from '../models/student';
import { Enrollment } from '../models/enrollment';
import { Observable } from 'rxjs';
import { Program } from '../models/program';
import { Course } from '../models/course';

@Injectable({
  providedIn: 'root'
})
export class StudyProgressService {

  private REST_API_SERVER="http://cadillacs-abp.kn01.fhict.nl/api/studyprogress";

  httpOptions = {
    headers: new HttpHeaders({ 'Content-Type': 'application/json' })
  };

  constructor(private httpClient:HttpClient) { }

  //Zone COURSES
  //Courses - GET
  public getProgramsOfCourse(courseID:string):Observable<Program[]>{
    return this.httpClient.get<Program[]>(`${this.REST_API_SERVER}/Courses/${courseID}/programs`);
  }

  public getRequiredCoursesOfCourse(courseID:string):Observable<Course[]>{
    return this.httpClient.get<Course[]>(`${this.REST_API_SERVER}/Courses/${courseID}/requirements`);
  }

  //Courses - POST

  //Courses - DELETE
  public deleteCourseProgram(course:Course | string, program:Program | string): Observable<Program>{
    const courseId = typeof course === 'string' ? course : course.id;
    const id = typeof program === 'string' ? program : program.id;

    return this.httpClient.delete<Program>(`${this.REST_API_SERVER}/Courses/${courseId}/programs/${id}`, this.httpOptions);
  }

  public deleteCourseRequirement(course:Course | string, requiredCourse:Course | string): Observable<Course>{
    const courseId = typeof course === 'string' ? course : course.id;
    const id = typeof requiredCourse === 'string' ? requiredCourse : requiredCourse.id;

    return this.httpClient.delete<Course>(`${this.REST_API_SERVER}/Courses/${courseId}/requirements/${id}`, this.httpOptions);
  }

  //End Zone COURSES

  //*****************//

  //Zone PROGRAMS
  //Programs - GET
  public getPrograms():Observable<Program[]>{
    return this.httpClient.get<Program[]>(`${this.REST_API_SERVER}/Programs`);
  }

  public getProgram(programID):Observable<Program>{
    return this.httpClient.get<Program>(`${this.REST_API_SERVER}/Programs/${programID}`);
  }

  public getCoursesInProgram(programID):Observable<Course[]>{
    return this.httpClient.get<Course[]>(`${this.REST_API_SERVER}/Programs/${programID}/courses`);
  }

  //Programs - PUT
  public updateProgram(program:Program):Observable<any> {
    return this.httpClient.put(`${this.REST_API_SERVER}/Programs/${program.id}`, 
      {
        "name": program.name,
        "description": program.description,
        "totalCredit": program.totalCredit
      }, 
      this.httpOptions)
  }

  //Programs - POST

  //Programs - DELETE
  public deleteProgram(program:Program | string): Observable<Program>{
    const id = typeof program === 'string' ? program : program.id;

    return this.httpClient.delete<Program>(`${this.REST_API_SERVER}/Programs/${id}`, this.httpOptions);
  }

  public deleteProgramCourse(program:Program | string, course:Course | string): Observable<Course>{
    const programId = typeof program === 'string' ? program : program.id;
    const id = typeof course === 'string' ? course : course.id;

    return this.httpClient.delete<Course>(`${this.REST_API_SERVER}/Programs/${id}/courses/${id}`, this.httpOptions);
  }

  //End Zone PROGRAMS

  //*****************//

  //Zone STUDENTS
  //Students - GET
  public getEnrollments(studentID:number):Observable<Enrollment[]>{
    return this.httpClient.get<Enrollment[]>(`${this.REST_API_SERVER}/Students/${studentID}/enrollments`);
  }
  //End Zone STUDENTS

  //*****************//

}
