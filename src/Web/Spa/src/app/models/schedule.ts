import { TimeSlot } from './timeslot';
import { Room } from './room';
import { Class } from './class';

export class Schedule{
    timeSlot:TimeSlot;
    date:Date;
    room:Room;
    class:Class;
}