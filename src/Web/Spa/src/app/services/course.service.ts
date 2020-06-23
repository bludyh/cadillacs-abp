import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Attachment } from '../models/attachment';
import { Course } from '../models/course';
import { Class } from '../models/class';
import { StudyMaterial } from '../models/studyMaterial';

@Injectable({
  providedIn: 'root'
})
export class CourseService {

  private REST_API_SERVER="http://cadillacs-abp.kn01.fhict.nl/api/course";

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

  //Courses - POST

  //Courses - DELETE

  //End Zone COURSES

  //*****************//
  
  //Zone TEACHERS
  //Teachers - GET

  //Teachers - PUT

  //Teachers - POST

  //Teachers - DELETE

  //End Zone TEACHERS

  //*****************//
}
