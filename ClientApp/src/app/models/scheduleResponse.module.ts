export interface ScheduleResponse {
    dayOfWeek: string;
    scheduleInfos: ScheduleInfo[];
}

export interface ScheduleInfo {
    isPresent: boolean;
    subject: Subject;
    snapshots: Snapshots[];
    totalTimePresence: string;
}

export interface Subject {
    teacherName: string;
    lecturerId: string;
    type: string;
    time: string;
    name: string;
    place: string;
    tag: string;
}

export interface Snapshots {
    id: string;
    time: string;
    student: StudentInfo;
}

export interface StudentInfo {
    id: string;
    fullName: string;
    course: number;
    group: string;
    imagePath: string; 
}