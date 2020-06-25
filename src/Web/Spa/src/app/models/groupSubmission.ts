import { Submission } from './submission';
import { Group } from './group';

export class GroupSubmission extends Submission {
    group: Group
  assignmentId: number;
  attachmentId: number;
  grade: number;
}
