import { Teacher } from './teacher';
import { Student } from './student';
import { Room } from './room';

export class TeacherMentor{
    mentorType:string;
    teacher:Teacher;
    room:Room;
}

export class StudentMentor{
    mentorType:string;
    student:Student;
}