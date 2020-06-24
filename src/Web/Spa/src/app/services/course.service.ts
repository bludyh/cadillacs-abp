import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Assignment } from '../models/assignment';
import { Attachment } from '../models/attachment';
import { Course } from '../models/course';
import { Class } from '../models/class';
import { Enrollment } from '../models/enrollment';
import { StudyMaterial } from '../models/studyMaterial';
import { StudentSubmission } from '../models/studentSubmission';
import { GroupSubmission } from '../models/groupSubmission';
import { Group } from '../models/group';

@Injectable({
  providedIn: 'root'
})
export class CourseService {

  private REST_API_SERVER="http://cadillacs-abp.kn01.fhict.nl/api/course";

  httpOptions = {
    headers: new HttpHeaders({ 'Content-Type': 'application/json' })
  };

  constructor(private httpClient:HttpClient) { }

  //Zone ATTACHMENTS
  //Attachments - GET
  public getAttachments():Observable<Attachment[]>{
    return this.httpClient.get<Attachment[]>(`${this.REST_API_SERVER}/Attachments`);
  }

  public getAttachment(attachmentID:number):Observable<Attachment>{
    return this.httpClient.get<Attachment>(`${this.REST_API_SERVER}/Attachments/${attachmentID}`);
  }

  //Attachments - PUT
  public updateAttachment(attachment:Attachment):Observable<any> {
    return this.httpClient.put(`${this.REST_API_SERVER}/Attachments/${attachment.id}`, 
      {
        "name": attachment.name,
        "path": attachment.path
      }, 
      this.httpOptions)
  }

  //Attachments - POST

  //Attachments - DELETE

  //End Zone ATTACHMENTS

  //*****************//

  //Zone COURSES
  //Courses - GET
  public getCourses():Observable<Course[]>{
    return this.httpClient.get<Course[]>(`${this.REST_API_SERVER}/Courses`);
  }

  public getCourse(courseID:string):Observable<Course>{
    return this.httpClient.get<Course>(`${this.REST_API_SERVER}/Courses/${courseID}`);
  }

  public getClassesInCourse(classCourseID:string):Observable<Class[]>{
    return this.httpClient.get<Class[]>(`${this.REST_API_SERVER}/Courses/${classCourseID}/classes`);
  }

  public getClassInCourse(classCourseID:string,classID:string,classSemester:number,classYear:number):Observable<Class>{
    return this.httpClient.get<Class>(`${this.REST_API_SERVER}/Courses/${classCourseID}/classes/${classID}/${classSemester}/${classYear}`);
  }

  public getStudyMaterialsOfCourses(classCourseID:string,classID:string,classSemester:number,classYear:number):Observable<StudyMaterial[]>{
    return this.httpClient.get<StudyMaterial[]>(`${this.REST_API_SERVER}/Courses/${classCourseID}/classes/${classID}/${classSemester}/${classYear}/study-materials`);
  }

  public getStudyMaterialOfCourses(classCourseID:string,classID:string,classSemester:number,classYear:number,studyMaterialID:number):Observable<StudyMaterial>{
    return this.httpClient.get<StudyMaterial>(`${this.REST_API_SERVER}/Courses/${classCourseID}/classes/${classID}/${classSemester}/${classYear}/study-materials/${studyMaterialID}`);
  }

  //Courses - PUT
  public updateCourse(course:Course):Observable<any> {
    return this.httpClient.put(`${this.REST_API_SERVER}/Courses/${course.id}`, 
      {
        "name": course.name,
        "description": course.description,
        "credit": course.credit
      }, 
      this.httpOptions)
  }

  public updateStudyMaterialOfCourse(classCourseID:string, classID:string, classSemester:number, classYear:number, studyMaterial:StudyMaterial):Observable<any> {
    return this.httpClient.put(`${this.REST_API_SERVER}/Courses/${classCourseID}/classes/${classID}/${classSemester}/${classYear}/study-materials/${studyMaterial.id}`, 
      {
        "name": studyMaterial.name,
        "description": studyMaterial.description,
        "week": studyMaterial.week
      }, 
      this.httpOptions)
  }

  public updateEnrollmentOfCourse(classCourseID:string, classID:string, classSemester:number, classYear:number, enrollment:Enrollment):Observable<any> {
    return this.httpClient.put(`${this.REST_API_SERVER}/Courses/${classCourseID}/classes/${classID}/${classSemester}/${classYear}/enrollments/${enrollment.student.id}`, 
      {
        "groupId": enrollment.group.id,
        "finalGrade": enrollment.finalGrade
      }, 
      this.httpOptions)
  }

  public updateAssignmentOfCourse(classCourseID:string, classID:string, classSemester:number, classYear:number, assignment:Assignment):Observable<any> {
    return this.httpClient.put(`${this.REST_API_SERVER}/Courses/${classCourseID}/classes/${classID}/${classSemester}/${classYear}/assignments/${assignment.id}`, 
      {
        "name": assignment.name,
        "type": assignment.type,
        "description": assignment.description, 
        "deadlineDateTime": assignment.deadlineDateTime, 
        "weight": assignment.weight
      }, 
      this.httpOptions)
  }

  public updateStudentSubmissionOfCourse(classCourseID:string, classID:string, classSemester:number, classYear:number, assignmentID:number, studentSubmission:StudentSubmission):Observable<any> {
    return this.httpClient.put(`${this.REST_API_SERVER}/Courses/${classCourseID}/classes/${classID}/${classSemester}/${classYear}/assignments/${assignmentID}/student-submissions/${studentSubmission.id}`, 
      {
        "studentId": studentSubmission.student.id, 
        "assignmentId": studentSubmission.assignment.id, 
        "attachmentId": studentSubmission.attachment.id, 
        "grade": studentSubmission.grade
      }, 
      this.httpOptions)
  }

  public updateGroupSubmissionOfCourse(classCourseID:string, classID:string, classSemester:number, classYear:number, assignmentID:number, groupSubmission:GroupSubmission):Observable<any> {
    return this.httpClient.put(`${this.REST_API_SERVER}/Courses/${classCourseID}/classes/${classID}/${classSemester}/${classYear}/assignments/${assignmentID}/group-submissions/${groupSubmission.id}`, 
      {
        "groupId": groupSubmission.group.id, 
        "assignmentId": groupSubmission.assignment.id, 
        "attachmentId": groupSubmission.attachment.id, 
        "grade": groupSubmission.grade
      }, 
      this.httpOptions)
  }

  public updateGroupOfCourse(classCourseID:string, classID:string, classSemester:number, classYear:number, group:Group):Observable<any> {
    return this.httpClient.put(`${this.REST_API_SERVER}/Courses/${classCourseID}/classes/${classID}/${classSemester}/${classYear}/groups/${group.id}`, 
      {
        "name": group.name, 
        "maxSize": group.maxSize
      }, 
      this.httpOptions)
  }

  //Courses - POST

  //Courses - DELETE

  //End Zone COURSES

  //*****************//
  
  //Zone TEACHERS
  //Teachers - GET

  //Teachers - POST

  //Teachers - DELETE

  //End Zone TEACHERS

  //*****************//
}
