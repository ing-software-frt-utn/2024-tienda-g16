import { Component, OnInit } from '@angular/core';
import { OrdersService } from '../../services/orders.service';
import { Provider } from '../../../models/Provider';
import { Order } from '../../../models/Order';
import { forkJoin } from 'rxjs';
import { Venta } from '../../../models/Venta';

@Component({
  selector: 'app-orders-list',
  templateUrl: './orders-list.component.html',
  styleUrl: './orders-list.component.css'
})
export class OrdersListComponent implements OnInit {
  
  infoString:string ='';

  constructor(public orderServ : OrdersService){}
  
  ventas:Venta[]=[];
  

  ngOnInit(): void {
    this.orderServ.obtenerVentas().subscribe(res=>{
      this.ventas=res.ventas;
    })
  }
  
}
