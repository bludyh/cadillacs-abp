import { Assignment } from './assignment';
import { Attachment } from './attachment';

export class Submission {
    id: number;
    assignment: Assignment;
    attachment: Attachment;
    grade: number;
}