import { Component, Inject,OnInit } from '@angular/core';
import { Http } from '@angular/http';

@Component({
    selector: 'employee',
    templateUrl: './employee.component.html'
})
export class EmployeeComponent implements OnInit {
    public employeeDetails: EmployeeDetails[];
    http: Http;
    baseUrl: string;

    constructor(http: Http, @Inject('BASE_URL') baseUrl: string) {
        this.http = http;
        this.baseUrl = baseUrl;
    }

    ngOnInit() {
        this.http.get(this.baseUrl + 'api/Employee/GetEmployees').subscribe(result => {
            this.employeeDetails = result.json();
        }, error => console.error(error));
    }
}

export class EmployeeDetails {
    empoyeeId: string;
    companyId: string;
    employeeName: string;
    email: string;
    dni: string;
    companyName: string;
    companyEmail: string;
    description: string;
    nIF: string;
    constructor() {
        this.empoyeeId = '';
        this.companyId = '';
        this.employeeName = '';
        this.email = '';
        this.dni = '';
        this.companyName = '';
        this.companyEmail = '';
        this.description = '';
        this.nIF = '';
    }
}
