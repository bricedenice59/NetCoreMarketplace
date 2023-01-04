import { Component, Input, OnInit } from '@angular/core';

@Component({
  selector: 'app-shop-paging-header',
  templateUrl: './shop-paging-header.component.html',
  styleUrls: ['./shop-paging-header.component.scss'],
})
export class ShopPagingHeaderComponent implements OnInit {
  @Input() pageIndex: number = 1;
  @Input() pageSize: number = 1;
  @Input() totalCount: number | undefined;

  ngOnInit(): void {}
}
