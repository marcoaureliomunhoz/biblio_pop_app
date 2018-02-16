import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {

  show = 0;
  menu_registrar = 0;
  menu_consultar = 0;

  toggleCollapse() {
    if (this.show === 0) {
      this.show = 1;
    } else {
      this.show = 0;
    }
  }

  menuRegistrar() {
    this.menu_consultar = 0;
    if (this.menu_registrar === 0) {
      this.menu_registrar = 1;
    } else {
      this.menu_registrar = 0;
    }
  }

  menuConsultar() {
    this.menu_registrar = 0;
    if (this.menu_consultar === 0) {
      this.menu_consultar = 1;
    } else {
      this.menu_consultar = 0;
    }
  }

  constructor() { }

  ngOnInit() {
  }

}
