import { Submission } from './submission';
import { Student } from './student';

export class StudentSubmission extends Submission {
    student: Student
    assignmentId: number;
    attachmentId: number;
    grade: number;
}
