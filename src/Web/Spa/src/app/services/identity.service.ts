import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
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
import {Attachment} from "../models/attachment";

@Injectable({
  providedIn: 'root'
})
export class IdentityService {

  private REST_API_SERVER="http://cadillacs-abp.kn01.fhict.nl/api/identity";

  httpOptions = {
    headers: new HttpHeaders({ 'Content-Type': 'application/json' })
  };

  constructor(private httpClient:HttpClient) { }

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
  public ppstBuilding(building:Building) {
    this.httpClient.post(`${this.REST_API_SERVER}/Buildings`,
      `"${building.id}"`
      ,this.httpOptions).subscribe(
      data => {

      },
      error => {
        console.log("Error", error);
      }
    );
  }

  public ppstBuildingRoom(building:Building) {
    this.httpClient.post(`${this.REST_API_SERVER}/Buildings/${building.id}/rooms`,
      `"${building.id}"`
      ,this.httpOptions).subscribe(
      data => {

      },
      error => {
        console.log("Error", error);
      }
    );
  }

  public postBuildingSchool(building:Building) {
    this.httpClient.post(`${this.REST_API_SERVER}/Buildings/${building.id}/schools`,
      `"${building.id}"`
      ,this.httpOptions).subscribe(
      data => {

      },
      error => {
        console.log("Error", error);
      }
    );
  }

  //Buildings - DELETE
  public deleteBuilding(building:Building | string): Observable<Building>{
    const id = typeof building === 'string' ? building : building.id;

    return this.httpClient.delete<Building>(`${this.REST_API_SERVER}/Buildings/${id}`, this.httpOptions);
  }

  public deleteBuildingRoom(building:Building | string, room:Room | string): Observable<Room>{
    const buildingId = typeof building === 'string' ? building : building.id;
    const id = typeof room === 'string' ? room : room.id;

    return this.httpClient.delete<Room>(`${this.REST_API_SERVER}/Buildings/${buildingId}/rooms/${id}`, this.httpOptions);
  }

  public deleteBuildingSchool(building:Building | string, school:School | string): Observable<School>{
    const buildingId = typeof building === 'string' ? building : building.id;
    const id = typeof school === 'string' ? school : school.id;

    return this.httpClient.delete<School>(`${this.REST_API_SERVER}/Buildings/${buildingId}/schools/${id}`, this.httpOptions);
  }

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
  public postEmployees(employee:Employee) {
    this.httpClient.post(`${this.REST_API_SERVER}/Employees`,
      {
        "firstName": `"${employee.firstName}"`,
        "lastName": `"${employee.lastName}"`,
        "initials": `"${employee.initials}"`,
        "email": `"${employee.email}"`,
        "phoneNumber": `"${employee.phoneNumber}"`,
        "schoolId": "string",
        "roomId": "string",
        "buildingId": "string"}
      ,this.httpOptions).subscribe(
      data => {

      },
      error => {
        console.log("Error", error);
      }
    );
  }

  //Employees - PUT
  public updateEmployee(employee:Employee):Observable<any> {
    return this.httpClient.put(`${this.REST_API_SERVER}/Employees/${employee.id}`,
      {
        "firstName": employee.firstName,
        "lastName": employee.lastName,
        "initials": employee.initials,
        "phoneNumber": employee.phoneNumber,
        "schoolId": employee.school.id,
        "roomId": employee.room.id,
        "buildingId": employee.building.id
      },
      this.httpOptions)
  }

  //Employees - DELETE
  public deleteEmployee(employee:Employee | number): Observable<Employee>{
    const id = typeof employee === 'number' ? employee : employee.id;

    return this.httpClient.delete<Employee>(`${this.REST_API_SERVER}/Employees/${id}`, this.httpOptions);
  }

  public deleteEmployeeRole(employee:Employee | number, role:Role | string): Observable<Role>{
    const employeeId = typeof employee === 'number' ? employee : employee.id;
    const id = typeof role === 'string' ? role : role.name;

    return this.httpClient.delete<Role>(`${this.REST_API_SERVER}/Employees/${employeeId}/roles/${id}`, this.httpOptions);
  }

  public deleteEmployeeProgram(employee:Employee | number, program:Program | string): Observable<Program>{
    const employeeId = typeof employee === 'number' ? employee : employee.id;
    const id = typeof program === 'string' ? program : program.id;

    return this.httpClient.delete<Program>(`${this.REST_API_SERVER}/Employees/${employeeId}/programs/${id}`, this.httpOptions);
  }

  //End Zone EMPLOYEES

  //*****************//

  //Zone PROGRAMS
  //Programs - GET
  public getEmployeesInProgram(programID:number):Observable<Employee[]>{
    return this.httpClient.get<Employee[]>(`${this.REST_API_SERVER}/Programs/${programID}/employees`);
  }
  //Programs - POST

  //Programs - DELETE
  public deleteProgramEmployee(program:Program | string, employee:Employee | number): Observable<Employee>{
    const programId = typeof program === 'string' ? program : program.id;
    const id = typeof employee === 'number' ? employee : employee.id;

    return this.httpClient.delete<Employee>(`${this.REST_API_SERVER}/Programs/${programId}/employees/${id}`, this.httpOptions);
  }

  //End Zone PROGRAMS

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
  public updateSchool(school:School):Observable<any> {
    return this.httpClient.put(`${this.REST_API_SERVER}/Schools/${school.id}`,
      {
        "name": school.name,
        "streetName": school.streetName,
        "houseNumber": school.houseNumber,
        "postalCode": school.postalCode,
        "city": school.city,
        "country": school.country
      },
      this.httpOptions)
  }

  //Schools - DELETE
  public deleteSchool(school:School | string): Observable<School>{
    const id = typeof school === 'string' ? school : school.id;

    return this.httpClient.delete<School>(`${this.REST_API_SERVER}/Schools/${id}`, this.httpOptions);
  }

  public deleteSchoolBuilding(school:School | string, building:Building | string): Observable<Building>{
    const schoolId = typeof school === 'string' ? school : school.id;
    const id = typeof building === 'string' ? building : building.id;

    return this.httpClient.delete<Building>(`${this.REST_API_SERVER}/Schools/${schoolId}/buildings/${id}`, this.httpOptions);
  }

  //End Zone SCHOOLS

  //*****************//

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
  public updateStudent(student:Student):Observable<any> {
    return this.httpClient.put(`${this.REST_API_SERVER}/Students/${student.id}`,
      {
        "firstName": student.firstName,
        "lastName": student.lastName,
        "initials": student.initials,
        "phoneNumber": student.phoneNumber,
        "streetName": student.streetName,
        "houseNumber": student.houseNumber,
        "postalCode": student.postalCode,
        "city": student.city,
        "country": student.country,
        "programId": student.program.id
      },
      this.httpOptions)
  }

  //Students - DELETE
  public deleteStudent(student:Student | number): Observable<Student>{
    const id = typeof student === 'number' ? student : student.id;

    return this.httpClient.delete<Student>(`${this.REST_API_SERVER}/Students/${id}`, this.httpOptions);
  }

  public deleteStudentRole(student:Student | number, role:Role | string): Observable<Role>{
    const studentId = typeof student === 'number' ? student : student.id;
    const id = typeof role === 'string' ? role : role.name;

    return this.httpClient.delete<Role>(`${this.REST_API_SERVER}/Students/${studentId}/roles/${id}`, this.httpOptions);
  }

  public deleteStudentMentor(student:Student | number, teacherId:number, mentorType:string): Observable<any>{
    const studentId = typeof student === 'number' ? student : student.id;

    return this.httpClient.delete<any>(`${this.REST_API_SERVER}/Students/${studentId}/mentors?teacherId=${teacherId}&mentorType=${mentorType}`, this.httpOptions);
  }

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
  public updateTeacher(teacher:Teacher):Observable<any> {
    return this.httpClient.put(`${this.REST_API_SERVER}/Teachers/${teacher.id}`,
      {
        "firstName": teacher.firstName,
        "lastName": teacher.lastName,
        "initials": teacher.initials,
        "phoneNumber": teacher.phoneNumber,
        "schoolId": teacher.school.id,
        "roomId": teacher.room.id,
        "buildingId": teacher.building.id
      },
      this.httpOptions)
  }

  //Teachers - DELETE
  public deleteTeacher(teacher:Teacher | number): Observable<Teacher>{
    const id = typeof teacher === 'number' ? teacher : teacher.id;

    return this.httpClient.delete<Teacher>(`${this.REST_API_SERVER}/Teachers/${id}`, this.httpOptions);
  }

  public deleteTeacherRole(teacher:Teacher | number, role:Role | string): Observable<Role>{
    const teacherId = typeof teacher === 'number' ? teacher : teacher.id;
    const id = typeof role === 'string' ? role : role.name;

    return this.httpClient.delete<Role>(`${this.REST_API_SERVER}/Teachers/${teacherId}/roles/${id}`, this.httpOptions);
  }

  public deleteTeacherProgram(teacher:Teacher | number, program:Program | string): Observable<Program>{
    const teacherId = typeof teacher === 'number' ? teacher : teacher.id;
    const id = typeof program === 'string' ? program : program.id;

    return this.httpClient.delete<Program>(`${this.REST_API_SERVER}/Teachers/${teacherId}/programs/${id}`, this.httpOptions);
  }

  public deleteTeacherMentor(teacher:Teacher | number, studentId:number, mentorType:string): Observable<any>{
    const teacherId = typeof teacher === 'number' ? teacher : teacher.id;

    return this.httpClient.delete<any>(`${this.REST_API_SERVER}/Teachers/${teacherId}/mentors?studentId=${studentId}&mentorType=${mentorType}`, this.httpOptions);
  }

  //End Zone TEACHERS

  //*****************//
}
