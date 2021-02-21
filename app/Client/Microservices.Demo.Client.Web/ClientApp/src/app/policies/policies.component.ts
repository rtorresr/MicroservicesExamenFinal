import { Component, OnInit } from '@angular/core';
import { PoliciesService } from '../services/data/policies/policies.service';
import { IReporte } from '../models/ireportes';

const ELEMENT_DATA: IReporte[] = [
  {NombreProducto: 'Safe Traveller', Descripcion: 'Travel insurance', CodigoProducto: 'TRI', PolicyStatusId: '1', NombreFoto: '/assets/products/travel.jpg'},
  {NombreProducto: 'Happy House', Descripcion: 'House insurance', CodigoProducto: 'HSI', PolicyStatusId: '1', NombreFoto: '/assets/products/house.jpg'},
  {NombreProducto: 'Happy Farm', Descripcion: 'Farm insurance', CodigoProducto: 'FAI', PolicyStatusId: '1', NombreFoto: '/assets/products/farm.jpg'}
];

@Component({
  selector: 'app-policies',
  templateUrl: './policies.component.html',
  styleUrls: ['./policies.component.scss']
})
export class PoliciesComponent implements OnInit {
  reportes: IReporte[] = [];
  displayedColumns: string[] = ['CodigoProducto', 'NombreProducto', 'Descripcion', 'PolicyStatusId'];
  dataSource = ELEMENT_DATA;

  constructor(
    private policiesService: PoliciesService,
  ) { }

  ngOnInit(): void {
    this.getReportes();
  }

  getReportes() {
    this.policiesService.getReporte()
      .subscribe((response: IReporte[]) => {
        this.reportes = response;
        console.log(response);
      });
  }
}