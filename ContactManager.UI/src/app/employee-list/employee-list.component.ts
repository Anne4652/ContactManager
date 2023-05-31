import { Component, OnInit } from '@angular/core';
import { EmployeeService } from './employee-list.service';
import { Employee } from '../employee';

@Component({
  selector: 'app-employee-list',
  templateUrl: './employee-list.component.html',
  styleUrls: ['./employee-list.component.css'],
})
export class EmployeeListComponent implements OnInit {
  employees: Employee[] = [];
  filteredEmployees: Employee[] = [];
  filterText: string;
  sortBy: string;
  sortAsc: boolean;

  constructor(private employeeService: EmployeeService) {
    this.filterText = '';
    this.sortBy = '';
    this.sortAsc = true;
  }

  ngOnInit() {
    this.getEmployees();
  }

  getEmployees() {
    this.employeeService.getEmployees().subscribe(
      (employees) => {
        this.employees = employees;
      },
      (error) => {
        console.error('Error:', error);
      }
    );
  }

  uploadFile(event: any) {
    const file = event.target.files[0];
    if (file) {
      this.employeeService.uploadCsv(file).subscribe(
        (data) => {
          this.getEmployees();
        },
        (error) => {
          console.error('Error:', error);
        }
      );
    }
  }

  deleteEmployee(id: number) {
    if (confirm('Are you sure you want to delete this employee?')) {
      this.employeeService.deleteEmployee(id).subscribe(
        (data) => {
          this.getEmployees();
        },
        (error) => {
          console.error('Error:', error);
        }
      );
    }
  }

  updateEmployee(employee: any) {
    this.employeeService.updateEmployee(employee).subscribe(
      (data) => {},
      (error) => {
        console.error('Error:', error);
      }
    );
  }

  applyFilter() {
    if (this.filterText || this.filterText != '') {
      this.filteredEmployees = this.employees.filter((employee) =>
        employee.name.toLowerCase().includes(this.filterText.toLowerCase())
      );
    } else {
      this.filteredEmployees = this.employees;
    }
  }

  sortTable(column: string) {
    if (column === this.sortBy) {
      this.sortAsc = !this.sortAsc;
    } else {
      this.sortBy = column;
      this.sortAsc = true;
    }
    this.filteredEmployees.sort((a: any, b: any) => {
      const valueA = a[column];
      const valueB = b[column];
      if (valueA < valueB) {
        return this.sortAsc ? -1 : 1;
      } else if (valueA > valueB) {
        return this.sortAsc ? 1 : -1;
      } else {
        return 0;
      }
    });
  }

  onCellBlur(employee: any, field: string, value: any) {
    employee[field] = value;
    this.updateEmployee(employee);
  }
}
