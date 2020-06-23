import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Student } from '../models/student';
import { Role } from '../models/role';
import { TeacherMentor } from '../models/mentor';
import { StudentMentor } from '../models/mentor';
import { Building } from '../models/building';
import { Room } from '../models/room';
import { School } from '../models/school';
import { Employee } from '../models/employee';
import { Program } from '../models/program';
import { Teacher } from '../models/teacher';

@Injectable({
  providedIn: 'root'
})
export class IdentityService {

  private REST_API_SERVER="http://cadillacs-abp.kn01.fhict.nl/api/identity";
  
  constructor(private httpClient:HttpClient) { }

  //Zone STUDENTS
  //StudentS - GET
  public getStudent(studentID:number):Observable<Student>{
    return this.httpClient.get<Student>(`${this.REST_API_SERVER}/Students/${studentID}`);
  }

  public getStudents():Observable<Student[]>{
    return this.httpClient.get<Student[]>(`${this.REST_API_SERVER}/Students`);
  }

  public getStudentRole(studentID:number):Observable<Role[]>{
    return this.httpClient.get<Role[]>(`${this.REST_API_SERVER}/Students/${studentID}/roles`);
  }

  public getStudentMentors(studentID:number):Observable<TeacherMentor[]>{
    return this.httpClient.get<TeacherMentor[]>(`${this.REST_API_SERVER}/Students/${studentID}/mentors`);
  }

  //Students - POST

  //Students - PUT

  //Students - DELETE

  //End Zone STUDENTS

  //*****************//

  //Zone TEACHERS
  //Teachers - GET
  public getTeachers():Observable<Teacher[]>{
    return this.httpClient.get<Teacher[]>(`${this.REST_API_SERVER}/Teachers`);
  }

  public getTeacher(teacherID):Observable<Teacher>{
    return this.httpClient.get<Teacher>(`${this.REST_API_SERVER}/Teachers/${teacherID}`);
  }

  public getTeacherRoles(teacherID):Observable<Role[]>{
    return this.httpClient.get<Role[]>(`${this.REST_API_SERVER}/Teachers/${teacherID}/roles`);
  }

  public getTeacherPrograms(teacherID):Observable<Program[]>{
    return this.httpClient.get<Program[]>(`${this.REST_API_SERVER}/Teachers/${teacherID}/programs`);
  }

  public getTeacherMentors(teacherID):Observable<StudentMentor[]>{
    return this.httpClient.get<StudentMentor[]>(`${this.REST_API_SERVER}/Teachers/${teacherID}/mentors`);
  }

  //Teachers - POST

  //Teachers - PUT

  //Teachers - DELETE

  //End Zone TEACHERS

  //*****************//

  //Zone SCHOOLS
  //Schools - GET
  public getSchools():Observable<School[]>{
    return this.httpClient.get<School[]>(`${this.REST_API_SERVER}/Schools`);
  }

  public getSchool(schoolID:string):Observable<School>{
    return this.httpClient.get<School>(`${this.REST_API_SERVER}/Schools/${schoolID}`);
  }

  public getBuildingsInSchool(schoolID:string):Observable<Building[]>{
    return this.httpClient.get<Building[]>(`${this.REST_API_SERVER}/Schools/${schoolID}/buildings`);
  }

  //Schools - POST

  //Schools - PUT

  //Schools - DELETE

  //End Zone SCHOOLS

  //*****************//

  //Zone PROGRAMS
  //Programs - GET
  public getEmployeesInProgram(programID:number):Observable<Employee[]>{
    return this.httpClient.get<Employee[]>(`${this.REST_API_SERVER}/Programs/${programID}/employees`);
  }
  //Programs - POST

  //Programs - DELETE

  //End Zone PROGRAMS

  //*****************//

  //Zone EMPLOYEES
  //Employees - GET
  public getEmployees():Observable<Employee[]>{
    return this.httpClient.get<Employee[]>(`${this.REST_API_SERVER}/Employees`);
  }

  public getEmployee(employeeID:number):Observable<Employee>{
    return this.httpClient.get<Employee>(`${this.REST_API_SERVER}/Employees/${employeeID}`);
  }

  public getEmployeeRoles(employeeID:number):Observable<Role[]>{
    return this.httpClient.get<Role[]>(`${this.REST_API_SERVER}/Employees/${employeeID}/roles`);
  }

  public getEmployeePrograms(employeeID:number):Observable<Program[]>{
    return this.httpClient.get<Program[]>(`${this.REST_API_SERVER}/Employees/${employeeID}/programs`);
  }

  //Employees - POST

  //Employees - PUT

  //Employees - DELETE

  //End Zone EMPLOYEES

  //*****************//

  //Zone BUILDINGS
  //Buildings - GET
  public getBuildings():Observable<Building[]>{
    return this.httpClient.get<Building[]>(`${this.REST_API_SERVER}/Buildings`);
  }

  public getBuilding(buildingID:string):Observable<Building>{
    return this.httpClient.get<Building>(`${this.REST_API_SERVER}/Buildings/${buildingID}`);
  }

  public getBuildingRooms(buildingID:string):Observable<Room[]>{
    return this.httpClient.get<Room[]>(`${this.REST_API_SERVER}/Buildings/${buildingID}/rooms`);
  }

  public getBuildingRoom(buildingID:string,roomID:string):Observable<Room>{
    return this.httpClient.get<Room>(`${this.REST_API_SERVER}/Buildings/${buildingID}/rooms/${roomID}`);
  }

  public getBuildingSchools(buildingID:string):Observable<School[]>{
    return this.httpClient.get<School[]>(`${this.REST_API_SERVER}/Buildings/${buildingID}/schools`);
  }

  //Buildings - POST

  //Buildings - PUT

  //Buildings - DELETE

  //End Zone BUILDINGS
}
