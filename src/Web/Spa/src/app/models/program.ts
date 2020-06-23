import { School } from './school';

export class Program{
    id:string;
    name:string;
    school:School;
}

export class DetailProgram{
    id:string;
    name:string;
    description:string;
    totalCredit:number;
    school:School;
}